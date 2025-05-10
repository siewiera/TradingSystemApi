using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using System.Transactions;
using TradingSystemApi.Entities.Documents.Receipts;
using TradingSystemApi.Interface.RepositoriesInterface;
using TradingSystemApi.Interface.ServicesInterface;
using TradingSystemApi.Models.ReceiptSale;
using TradingSystemApi.Models.ReceiptSaleItem;
using TradingSystemApi.Repositories;

namespace TradingSystemApi.Services
{

    public class ReceiptSaleService : IReceiptSaleService
    {
        private readonly IMapper _mapper;
        private readonly IStoreRepository _storeRepository;
        private readonly ISellerRepository _sellerRepository;
        private readonly ICashierRepository _cashierRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDocumentRepository<ReceiptSale> _documentRepository;
        private readonly IDocumentItemRepository<ReceiptSaleItem> _documentItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly IReceiptSaleItemService _receiptSaleItemService;
        private readonly ITransactionRepository _transactionRepository;

        public ReceiptSaleService
            (
                IMapper mapper, 
                IStoreRepository storeRepository, 
                ISellerRepository sellerRepository, 
                ICashierRepository cashierRepository, 
                ICustomerRepository customerRepository, 
                IDocumentRepository<ReceiptSale> documentRepository,
                IDocumentItemRepository<ReceiptSaleItem> documentItemRepository,
                IProductRepository productRepository,
                IProductService productService, 
                IReceiptSaleItemService receiptSaleItemService,
                ITransactionRepository transactionRepository
            )
        {
            _mapper = mapper;
            _storeRepository = storeRepository;
            _sellerRepository = sellerRepository;
            _cashierRepository = cashierRepository;
            _customerRepository = customerRepository;
            _documentRepository = documentRepository;
            _documentItemRepository = documentItemRepository;
            _productRepository = productRepository;
            _productService = productService;
            _receiptSaleItemService = receiptSaleItemService;
            _transactionRepository = transactionRepository;
        }

        public async Task ValidData(int storeId, int sellerId, int cashierId)
        {
            await _storeRepository.CheckStoreById(storeId);
            await _sellerRepository.CheckSellerById(storeId, sellerId);
            await _cashierRepository.GetCashierDataById(storeId, sellerId, cashierId);
        }

        /**/

        //Tworzenie paragonu z pozycjami
        public async Task<int> AddNewReceiptSale(AddNewReceiptSaleDto dto, int storeId, int sellerId, int cashierId)
        {
            await ValidData(storeId, sellerId, cashierId);
            using var transaction = await _transactionRepository.StartTransaction();
            decimal total = 0;
            int receiptSaleId = -1;

            dto.AddNewReceiptSaleItemDtos = dto.AddNewReceiptSaleItemDtos
                .GroupBy(x => x.ProductId)
                .Select(g => new AddNewReceiptSaleItemDto
                {
                    ProductId = g.Key,
                    Quantity = g.Sum(x => x.Quantity),
                })
                .ToList();

            try
            {
                foreach (var item in dto.AddNewReceiptSaleItemDtos)
                {
                    var product = await _productRepository.GetProductDataById(storeId, item.ProductId);
                    item.CostNetPrice = product.SellingPrice;
                    total += item.Quantity * product.SellingPrice;

                    await _productService.UpdateProductQuantity(storeId, item.ProductId, -item.Quantity);
                }

                var receiptSale = _mapper.Map<ReceiptSale>(dto);

                receiptSale.TotalAmount = total;
                receiptSale.DateOfIssue = DateTime.Now;
                receiptSale.DateOfSale = DateTime.Now;
                receiptSale.StoreId = storeId;
                receiptSale.CashierId = cashierId;

                await _documentRepository.Add(receiptSale);
                await _storeRepository.SaveChanges();
                receiptSaleId = receiptSale.Id;
                await transaction.CommitAsync();
            }
            catch (Exception ex) 
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error: {ex.Message}"); 
            }

            return receiptSaleId;
        }

        public async Task UpdateReceiptSaleDataById(UpdateReceiptSaleDto dto, int storeId, int sellerId, int cashierId, int receiptSaleId, int receiptSaleItemId = -1)
        {
            await ValidData(storeId, sellerId, cashierId);
            var receiptSale = await _documentRepository.GetById(storeId, receiptSaleId);
            using var transaction = await _transactionRepository.StartTransaction();

            try
            {
                //receiptSale.DateOfIssue = DateTime.Now;
                //var receiptSaleItem = await _salesDocumentItem.GetSalesDocumentItemById(storeId, receiptSaleId, receiptSaleItemId);

                //if (dto.UpdateReceiptSaleItemDto.ProductId == null && receiptSaleItemId != -1)
                //{


                //    await AddNewReceiptSale(newReceiptSale, storeId, sellerId, cashierId);

                //}
                //if (dto.UpdateReceiptSaleItemDto.ProductId != receiptSaleItem.ProductId)
                //{
                //    var receiptItemUpdate = await _receiptSaleItemService.GetReceiptItemUpdate(dto.UpdateReceiptSaleItemDto, storeId, receiptSaleId, receiptSaleItemId);

                //    var receiptSaleItemDto = _mapper.Map<SalesDocumentItem>(receiptItemUpdate);
                //    receiptSale.SalesDocumentItems.Add(receiptSaleItemDto);
                //}
                

                await _storeRepository.SaveChanges();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error: {ex.Message}");
            }

            //receiptSaleItem

            await _documentRepository.Update(receiptSale);
        }

        public async Task DeleteReceiptSaleById(int storeId, int sellerId, int cashierId, int receiptSaleId)
        {
            await ValidData(storeId, sellerId, cashierId);
            var receiptSale = await _documentRepository.GetById(storeId, receiptSaleId);
            await _documentRepository.Delete(receiptSale);
        }

        public async Task<ReceiptSaleDto> GetReceiptSaleById(int storeId, int sellerId, int cashierId, int receiptSaleId)
        {
            await ValidData(storeId, sellerId, cashierId);
            var receiptSale = await _documentRepository.GetById(storeId, receiptSaleId);
            var receiptSaleDto = _mapper.Map<ReceiptSaleDto>(receiptSale);

            return receiptSaleDto;
        }

        public async Task<ReceiptSaleDto> GetReceiptSaleByInvoiceNumber(int storeId, int sellerId, int cashierId, string invoiceNo)
        {
            await ValidData(storeId, sellerId, cashierId);
            var receiptSale = await _documentRepository.GetByInvoiceNumber(storeId, invoiceNo);
            var receiptSaleDto = _mapper.Map<ReceiptSaleDto>(receiptSale);

            return receiptSaleDto;
        }

        public async Task<IEnumerable<ReceiptSaleDto>> GetAllReceiptsSaleByCashierId(int storeId, int sellerId, int cashierId)
        {
            await ValidData(storeId, sellerId, cashierId);
            var receiptsSale = await _documentRepository.GetAllByCashierId(storeId, cashierId);
            var receiptsSaleDto = _mapper.Map<IEnumerable<ReceiptSaleDto>>(receiptsSale);

            return receiptsSaleDto;
        }

        public async Task<IEnumerable<ReceiptSaleDto>> GetAllReceiptsSale(int storeId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var receiptsSale = await _documentRepository.GetAll(storeId);
            var receiptsSalesDto = _mapper.Map<IEnumerable<ReceiptSaleDto>>(receiptsSale);

            return receiptsSalesDto;
        }
    }
}
