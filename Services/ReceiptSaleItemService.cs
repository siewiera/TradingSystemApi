using AutoMapper;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using TradingSystemApi.Entities.Documents.Receipts;
using TradingSystemApi.Interface.RepositoriesInterface;
using TradingSystemApi.Interface.ServicesInterface;
using TradingSystemApi.Models.ReceiptSaleItem;
using TradingSystemApi.Repositories;

namespace TradingSystemApi.Services
{
    public class ReceiptSaleItemService : IReceiptSaleItemService
    {
        private readonly IMapper _mapper;
        private readonly IStoreRepository _storeRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly IDocumentRepository<ReceiptSale> _documentRepository;
        private readonly IDocumentItemRepository<ReceiptSaleItem> _documentItemRepository;
        private readonly ITransactionRepository _transactionRepository;

        public ReceiptSaleItemService
            (
                IMapper mapper, 
                IStoreRepository storeRepository,
                IProductRepository productRepository, 
                IProductService productService,
                IDocumentRepository<ReceiptSale> documentRepository, 
                IDocumentItemRepository<ReceiptSaleItem> documentItemRepository,
                ITransactionRepository transactionRepository
            )
        {
            _mapper = mapper;
            _storeRepository = storeRepository;
            _productRepository = productRepository;
            _productService = productService;
            _documentRepository = documentRepository;
            _documentItemRepository = documentItemRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<List<ReceiptSaleItem>> GroupinOfReceiptProductItems(List<ReceiptSaleItem> dtos) 
        {
            dtos = dtos
                .GroupBy(x => new { x.ProductId, x.CostNetPrice })
                .Select(g => new ReceiptSaleItem
                {
                    ProductId = g.Key.ProductId,
                    CostNetPrice = g.Key.CostNetPrice,
                    Quantity = g.Sum(x => x.Quantity),
                })
                .ToList();

            return dtos;
        }

        public async Task<List<ReceiptSaleItem>> UpdateReceiptProductList(List<ReceiptSaleItem> receiptSaleItemsMapping, int storeId, int receiptSaleId)
        {
            var receiptSaleItems = await _documentItemRepository.GetAllDocumentItemsByDocumentId(storeId, receiptSaleId);

            receiptSaleItemsMapping.AddRange(receiptSaleItems);
            var groupedNewItems = await GroupinOfReceiptProductItems(receiptSaleItemsMapping);
            await _documentItemRepository.DeleteAllItems(receiptSaleItems);

            return groupedNewItems;
        }

        /**/

        // Dodawanie nowej pozycji do istniejącego już paragonu
        public async Task AddNewItemToReceiptSale(List<AddNewReceiptSaleItemDto> dtos, int storeId, int receiptSaleId)
        {
            using var transaction = await _transactionRepository.StartTransaction();
            await _documentRepository.GetById(storeId, receiptSaleId);
            decimal total = 0;

            try 
            {
                foreach (var item in dtos)
                {
                    var product = await _productRepository.GetProductDataById(storeId, item.ProductId);
                    item.CostNetPrice = product.SellingPrice;
                    total += item.Quantity * product.SellingPrice;

                    await _productService.UpdateProductQuantity(storeId, item.ProductId, -item.Quantity);
                }

                total += await _documentItemRepository.GetItemSumCalculation(storeId, receiptSaleId);

                var receiptSaleItemsMapping = _mapper.Map<List<ReceiptSaleItem>>(dtos);
                var groupedNewItems = await UpdateReceiptProductList(receiptSaleItemsMapping, storeId, receiptSaleId);

                var receiptSale = await _documentRepository.GetById(storeId, receiptSaleId);
                receiptSale.TotalAmount = total;
                receiptSale.DateOfIssue = DateTime.Now;

                foreach (var group in groupedNewItems)
                    receiptSale.DocumentItems.Add(group);

                await _documentRepository.Update(receiptSale);
                await _storeRepository.SaveChanges();
            
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error: {ex.Message}");
            }

        }

        public async Task UpdateReceiptSaleItemDataById(UpdateReceiptSaleItemDto dto, int storeId, int receiptSaleId, int receiptSaleItemId)
        {
            using var transaction = await _transactionRepository.StartTransaction();
            await _documentRepository.GetById(storeId, receiptSaleId);
            var receiptSaleItems = await _documentItemRepository.GetAllDocumentItemsByDocumentId(storeId, receiptSaleId);
            var receiptSaleItem = await _documentItemRepository.GetDocumentItemById(storeId, receiptSaleId, receiptSaleItemId);
            int productId = dto.ProductId ?? receiptSaleItem.ProductId;
            decimal total = 0;

            try 
            {
                if (receiptSaleItem.ProductId != productId)
                {
                    var oldProduct = await _productRepository.GetProductDataById(storeId, receiptSaleItem.ProductId);
                    var newProduct = await _productRepository.GetProductDataById(storeId, productId);

                    //total += dto.Quantity * newProduct.SellingPrice + receiptSaleItems.Where(r => r.Id != receiptSaleItemId).Select(r => r.);
                    dto.CostNetPrice = newProduct.CostNetPrice;

                    //przywrócenie stanu produktu zwracanego
                    await _productService.UpdateProductQuantity(storeId, receiptSaleItem.ProductId, receiptSaleItem.Quantity);
                    //zaciąganie ze stanu nowego produktu
                    await _productService.UpdateProductQuantity(storeId, productId, dto.Quantity);
                }
                else if (receiptSaleItem.ProductId == productId)
                {
                    var quantity = receiptSaleItem.Quantity - dto.Quantity;
                    //receiptSaleItem.Quantity = quantity;
                    //wyrównanie stanu produktu
                    await _productService.UpdateProductQuantity(storeId, productId, quantity);
                }

                var receiptSaleItemsDto = _mapper.Map<List<ReceiptSaleItem>>(dto);
                receiptSaleItemsDto.AddRange(receiptSaleItems.Where(r => r.Id != receiptSaleItemId));
                var groupedItems = await GroupinOfReceiptProductItems(receiptSaleItemsDto);
                receiptSaleItems = groupedItems;
                receiptSaleItems.FirstOrDefault().Document.TotalAmount = groupedItems.Select(g => g.CostNetPrice * g.Quantity).Sum();

                await _documentItemRepository.UpdateItem(receiptSaleItems);
                await _storeRepository.SaveChanges();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error: {ex.Message}");
            }
        }

        //public async Task<UpdateReceiptSaleItemDto> GetReceiptItemUpdate(UpdateReceiptSaleItemDto dto, int storeId, int receiptSaleId, int receiptSaleItemId)
        //{
        //    await _document.GetSaleById(storeId, receiptSaleId);
        //    var receiptSaleItem = await _documentItem.GetSalesDocumentItemById(storeId, receiptSaleId, receiptSaleItemId);

        //    var productId = dto.ProductId ?? receiptSaleItem.ProductId;
        //    var product = await _productRepository.GetProductDataById(storeId, dto.ProductId ?? receiptSaleItem.ProductId);

        //    if (receiptSaleItem.ProductId != dto.ProductId)
        //    {
        //        await _productService.UpdateProductQuantity(storeId, receiptSaleItem.ProductId, receiptSaleItem.Quantity);
        //        receiptSaleItem.CostNetPrice = product.SellingPrice;
        //    }
               
        //    receiptSaleItem.Quantity = dto.Quantity ?? receiptSaleItem.Quantity;
        //    receiptSaleItem.ProductId = dto.ProductId ?? receiptSaleItem.ProductId;

        //    await _documentItem.UpdateSaleItem(receiptSaleItem);

        //    return receiptSaleItem;
        //}
    }
}
