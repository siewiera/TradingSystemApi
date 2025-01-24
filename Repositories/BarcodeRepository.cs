using Microsoft.EntityFrameworkCore;
using TradingSystemApi.Context;
using TradingSystemApi.Entities;
using TradingSystemApi.Exceptions;
using TradingSystemApi.Interface.RepositoriesInterface;

namespace TradingSystemApi.Repositories
{
    public class BarcodeRepository : IBarcodeRepository
    {
        private readonly TradingSystemDbContext _dbContext;

        public BarcodeRepository(TradingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task CheckBarcodeExists(Barcode barcode_, int storeId)
        {
            var barcode = await _dbContext
                .Barcodes
                .FirstOrDefaultAsync
                (
                    b =>
                    b.Code == barcode_.Code &&
                    b.StoreId == storeId
                );

            if (barcode != null)
                throw new ConflictException("Barcode exists");
        }

        /**/

        public async Task AddNewBarcode(Barcode barcode)
        {
            await _dbContext.Barcodes.AddAsync(barcode);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateBarcodeData(Barcode barcode)
        {
            _dbContext.Barcodes.Update(barcode);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteBarcode(Barcode barcode)
        {
            _dbContext.Barcodes.Remove(barcode);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Barcode> GetBarcodeDataById(int storeId, int barcodeId)
        {
            var barcode = await _dbContext
                .Barcodes
                .FirstOrDefaultAsync(b => b.StoreId == storeId && b.Id == barcodeId);

            if (barcode == null)
                throw new NotFoundException("Barcode not found");

            return barcode;
        }

        public async Task<Barcode> GetBarcodeDataByCode(int storeId, string code)
        {
            var barcode = await _dbContext
                .Barcodes
                .FirstOrDefaultAsync(b => b.StoreId == storeId && b.Code == code);

            if (barcode == null)
                throw new NotFoundException("Barcode not found");

            return barcode;
        }

        public async Task<IEnumerable<Barcode>> GetAllBarcodesData(int storeId)
        {
            var barcodes = await _dbContext
                .Barcodes
                .Where(b => b.StoreId == storeId)
                .ToListAsync();

            if (!barcodes.Any())
                throw new NotFoundException("Barcode not found");

            return barcodes;
        }
    }
}
