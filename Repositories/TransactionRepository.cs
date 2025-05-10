using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using TradingSystemApi.Context;
using TradingSystemApi.Interface.RepositoriesInterface;

namespace TradingSystemApi.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TradingSystemDbContext _dbContext;

        public TransactionRepository(TradingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IDbContextTransaction> StartTransaction()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }
    }
}
