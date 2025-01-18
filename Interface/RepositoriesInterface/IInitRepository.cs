using TradingSystemApi.Entities;

namespace TradingSystemApi.Interface.RepositoriesInterface
{
    public interface IInitRepository
    {
        Task<Seller> CheckInitDataExists();
    }
}