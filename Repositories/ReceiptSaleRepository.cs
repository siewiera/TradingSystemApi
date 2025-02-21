using Microsoft.EntityFrameworkCore;
using TradingSystemApi.Context;
using TradingSystemApi.Entities;

namespace TradingSystemApi.Repositories
{
    public class ReceiptSaleRepository
    {
        //private readonly TradingSystemDbContext _dbContext;

        //public ReceiptSaleRepository(TradingSystemDbContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}

        //public async Task AddNewReceiptSales(ReceiptSale receiptSale)
        //{
        //    await _dbContext.Rece.AddRangeAsync(receiptSale);
        //    await _dbContext.SaveChangesAsync();
        //}

        //public async Task UpdateReceiptSalesData(ReceiptSale  receiptSale) 
        //{
        //    _dbContext.SalesDocuments.Update(receiptSale);
        //    await _dbContext.SaveChangesAsync();
        //}

        //public async Task DeleteReceitpSales(ReceiptSale receiptSale) 
        //{
        //    _dbContext.Remove(receiptSale);
        //    await _dbContext.SaveChangesAsync();
        //}

        //public async Task<ReceiptSale> GetReceiptSaleDataById(int receitpSaleId) 
        //{
        //    var receiptSale = await _dbContext
        //        .SalesDocuments
        //        .Include(s => s.ReceiptSale)
        //}
    }
}
