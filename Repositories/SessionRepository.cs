using Microsoft.EntityFrameworkCore;
using TradingSystemApi.Context;
using TradingSystemApi.Entities;
using TradingSystemApi.Interface.RepositoriesInterface;
using TradingSystemApi.Models.Session;

namespace TradingSystemApi.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly TradingSystemDbContext _dbContext;

        public SessionRepository(TradingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CheckSessionExists(int storeId, Guid sessionGuid) 
        {
            var session = await _dbContext
                .Sessions
                .FirstOrDefaultAsync(s => s.StoreId == storeId && s.SessionGuid == sessionGuid);

            if (session != null)
                throw new Exception("Session exists");
        }

        /**/
        public async Task AddNewSession(Session session, int storeId)
        {
            await _dbContext.Sessions.AddAsync(session);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateSessionData(Session session)
        {
            _dbContext.Sessions.Update(session);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteSession(Session session)
        {
            _dbContext.Sessions.Remove(session);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Session> GetSessionDataById(int storeId, int sessionId)
        {
            var session = await _dbContext
                .Sessions
                .FirstOrDefaultAsync(s => s.StoreId == storeId && s.Id == sessionId);

            if (session == null)
                throw new Exception("Session not found");

            return session;
        }

        public async Task<Session> GetSessionDataBySessionGuid(int storeId, Guid sessionGuid)
        {
            var session = await _dbContext
                .Sessions
                .FirstOrDefaultAsync(s => s.StoreId == storeId && s.SessionGuid == sessionGuid);

            if (session == null)
                throw new Exception("Session not found");

            return session;
        }

        public async Task<Session> GetSessionDataByCashierId(int storeId, int cashierId)
        {
            var session = await _dbContext
                .Sessions
                .FirstOrDefaultAsync(s => s.StoreId == storeId && s.CashierId == cashierId);

            if (session == null)
                throw new Exception("Session not found");

            return session;
        }

        public async Task<IEnumerable<Session>> GetAllSessionData(int storeId)
        {
            var sessions = await _dbContext
                .Sessions
                .Where(s => s.StoreId == storeId)
                .ToListAsync();

            if (!sessions.Any())
                throw new Exception("Session not found");

            return sessions;
        }
    }
}
