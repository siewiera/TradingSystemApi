using TradingSystemApi.Entities.BusinessEntities.Seller;

namespace TradingSystemApi.Interface.RepositoriesInterface
{
    public interface IInitRepository
    {
        Task<Seller> CheckInitDataExists();
    }
}