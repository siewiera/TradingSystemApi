using AutoMapper;
using TradingSystemApi.Context;
using TradingSystemApi.Interface.ServicesInterface;

namespace TradingSystemApi.Services
{
    public class InvoiceSaleService
    {
        private readonly TradingSystemDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISellerService _sellerService;

        public InvoiceSaleService(TradingSystemDbContext dbContext, IMapper mapper, ISellerService sellerService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _sellerService = sellerService;
        }



        //public int AddNewInvoice() 
        //{

        //}
    }
}
