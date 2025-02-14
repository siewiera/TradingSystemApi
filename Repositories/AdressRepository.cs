using Microsoft.EntityFrameworkCore;
using TradingSystemApi.Context;
using TradingSystemApi.Entities;
using TradingSystemApi.Exceptions;
using TradingSystemApi.Interface.RepositoriesInterface;
using TradingSystemApi.Models.AdressDto;

namespace TradingSystemApi.Repositories
{
    public class AdressRepository : IAdressRepository
    {
        private readonly TradingSystemDbContext _dbContext;

        public AdressRepository(TradingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CheckAdressDataExists(Adress adress_, int storeId)
        {
            var adress = await _dbContext
                .Adresses
                .FirstOrDefaultAsync
                (
                    a =>
                    a.Street == adress_.Street &&
                    a.HouseNo == adress_.HouseNo &&
                    a.City == adress_.City &&
                    a.ZipCode == adress_.ZipCode &&
                    a.Country == adress_.Country &&
                    a.StoreId == storeId
                );

            if (adress != null)
                throw new ConflictException("Address exists");
        }

        /**/

        public async Task AddingNewAdress(Adress adress)
        {
            await _dbContext.Adresses.AddAsync(adress);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAdressData(Adress adress)
        {
            _dbContext.Adresses.Update(adress);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAdress(Adress adress)
        {
            _dbContext.Adresses.Remove(adress);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Adress>> GetAllAdressesData(int storeId)
        {
            var adresses = await _dbContext
                .Adresses
                .Include(a => a.Seller)
                .Include(a => a.Customer)
                .Where(a => a.StoreId == storeId)
                .ToListAsync();

            if (!adresses.Any())
                throw new NotFoundException("Adress not found");

            return adresses;
        }

        public async Task<Adress> GetAdressDataById(int storeId, int adressId)
        {
            var adress = await _dbContext
                .Adresses
                .Include(a => a.Seller)
                .Include(a => a.Customer)
                .FirstOrDefaultAsync(a => a.StoreId == storeId && a.Id == adressId);

            if (adress == null)
                throw new NotFoundException("Address not found");

            return adress;
        }
    }
}
