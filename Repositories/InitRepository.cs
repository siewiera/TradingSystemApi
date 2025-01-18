using Microsoft.EntityFrameworkCore;
using TradingSystemApi.Context;
using TradingSystemApi.Entities;
using TradingSystemApi.Exceptions;
using TradingSystemApi.Interface.RepositoriesInterface;
using TradingSystemApi.Models.AdressDto;
using TradingSystemApi.Models.InitData;

namespace TradingSystemApi.Repositories
{
    public class InitRepository : IInitRepository
    {
        private readonly TradingSystemDbContext _dbContext;

        public InitRepository(TradingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Seller> CheckInitDataExists()
        {
            var initData = await _dbContext
                .Sellers
                .Include(s => s.Store)
                .Include(s => s.Adress)
                .Include(s => s.Cashiers)
                .FirstOrDefaultAsync(s => _dbContext
                    .Cashiers
                    .Any(c => c.Username == "Admin"));

            return initData;
        }
    }
}

