using TradingSystemApi.Entities;

namespace TradingSystemApi.Interface.RepositoriesInterface
{
    public interface ISessionRepository
    {
        Task AddNewSession(Session session, int storeId);
        Task CheckSessionExistsByCashierId(int storeId, int sellerId, int cashierId);
        Task CheckSessionExistsBySessionGuid(int storeId, int sellerId, Guid sessionGuid);
        Task DeleteSession(Session session);
        Task<IEnumerable<Session>> GetAllSessionsData(int storeId, int sellerId);
        Task<Session> GetSessionDataByCashierId(int storeId, int sellerId, int cashierId);
        Task<Session> GetSessionDataBySessionGuid(int storeId, int sellerId, Guid sessionGuid);
        Task UpdateSessionData(Session session);
        Task RemoveInactiveSession(DateTime thresholdTime);
    }
}