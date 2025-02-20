using TradingSystemApi.Entities;
using TradingSystemApi.Models.Session;

namespace TradingSystemApi.Interface.ServicesInterface
{
    public interface ISessionService
    {
        Task<int> AddNewSession(int storeId, int sellerId, int cashierId, string ip);
        Task CheckData(int storeId, int sellerId, int cashierId = -1);
        Task DeleteSessionByCashierid(int storeId, int sellerId, int cashierId);
        Task ExtensionSessionByCashierId(int storeId, int sellerId, int cashierId);
        Task ExtensionSessionBySessionGuid(int storeId, int sellerId, Guid sessionGuid);
        Task<IEnumerable<SessionDto>> GetAllSessionsData(int storeId, int sellerId);
        Task<SessionDto> GetSessionDataByCashierId(int storeId, int sellerId, int cashierId);
        Task<SessionDto> GetSessionDataBySessionGuid(int storeId, int sellerId, Guid sessionGuid);
        Task RemoveInactiveSession();
        SessionSettings LoadingSessionSettingsFile();
    }
}