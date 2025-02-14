using TradingSystemApi.Entities;

namespace TradingSystemApi.Interface.RepositoriesInterface
{
    public interface ISessionRepository
    {
        Task AddNewSession(Session session, int storeId);
        Task DeleteSession(Session session);
        Task<IEnumerable<Session>> GetAllSessionData(int storeId);
        Task<Session> GetSessionDataByCashierId(int storeId, int cashierId);
        Task<Session> GetSessionDataById(int storeId, int sessionId);
        Task<Session> GetSessionDataBySessionGuid(int storeId, Guid sessionGuid);
        Task UpdateSessionData(Session session);
        Task CheckSessionExists(int storeId, Guid sessionGuid);
    }
}