using Microsoft.EntityFrameworkCore.Storage;

namespace TradingSystemApi.Interface.RepositoriesInterface
{
    public interface ITransactionRepository
    {
        Task<IDbContextTransaction> StartTransaction();
    }
}