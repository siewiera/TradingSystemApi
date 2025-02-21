using AutoMapper;
using TradingSystemApi.Entities;
using TradingSystemApi.Interface.RepositoriesInterface;

namespace TradingSystemApi.Services
{
    public class ReceiptSaleService
    {
        private readonly IMapper _mapper;
        private readonly IStoreRepository _storeRepository;
        private readonly ICashierRepository _cashierRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ISalesDocumentRepository<ReceiptSale> _salesDocument;

        public ReceiptSaleService(IMapper mapper,IStoreRepository storeRepository, ICashierRepository cashierRepository, ICustomerRepository customerRepository, ISalesDocumentRepository<ReceiptSale> salesDocument)
        {
            _mapper = mapper;
            _storeRepository = storeRepository;
            _cashierRepository = cashierRepository;
            _customerRepository = customerRepository;
            _salesDocument = salesDocument;
        }

        //public async Task<int> AddNewReceiptSale(ReceiptSale receiptSale, int storeId) 
        //{
        //    await _storeRepository.CheckStoreById(storeId);
        //}
    }
}
