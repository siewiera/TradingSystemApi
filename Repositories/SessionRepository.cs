using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using TradingSystemApi.Context;
using TradingSystemApi.Entities;
using TradingSystemApi.Exceptions;
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

        public async Task CheckSessionExistsBySessionGuid(int storeId, int sellerId, Guid sessionGuid)
        {
            var session = await _dbContext
                .Sessions
                .FirstOrDefaultAsync
                (s =>
                    s.StoreId == storeId &&
                    s.Cashier.SellerId == sellerId &&
                    s.SessionGuid == sessionGuid
                );

            if (session != null)
                throw new ConflictException("Session exists");
        }

        public async Task CheckSessionExistsByCashierId(int storeId, int sellerId, int cashierId)
        {
            var session = await _dbContext
                .Sessions
                .FirstOrDefaultAsync
                (s =>
                    s.StoreId == storeId &&
                    s.Cashier.SellerId == sellerId &&
                    s.CashierId == cashierId
                );

            if (session != null)
                throw new ConflictException("Session exists");
        }


        public async Task RemoveInactiveSession(DateTime thresholdTime)
        {
            var sessions = await _dbContext
                .Sessions
                .Where(s => s.LastAction <= thresholdTime)
                .ToListAsync();

            if (sessions.Any())
            {
                _dbContext.Sessions.RemoveRange(sessions);
                await _dbContext.SaveChangesAsync();
            }
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

        public async Task<Session> GetSessionDataBySessionGuid(int storeId, int sellerId, Guid sessionGuid)
        {
            var session = await _dbContext
                .Sessions
                .Include(s => s.Cashier)
                .FirstOrDefaultAsync
                (s =>
                    s.StoreId == storeId &&
                    s.Cashier.SellerId == sellerId &&
                    s.SessionGuid == sessionGuid
                );

            if (session == null)
                throw new NotFoundException("Session not found");

            return session;
        }

        public async Task<Session> GetSessionDataByCashierId(int storeId, int sellerId, int cashierId)
        {
            var session = await _dbContext
                .Sessions
                .Include(s => s.Cashier)
                .FirstOrDefaultAsync
                (s =>
                    s.StoreId == storeId &&
                    s.Cashier.SellerId == sellerId &&
                    s.CashierId == cashierId
                );

            if (session == null)
                throw new NotFoundException("Session not found");

            return session;
        }

        public async Task<IEnumerable<Session>> GetAllSessionsData(int storeId, int sellerId)
        {
            var sessions = await _dbContext
                .Sessions
                .Include(s => s.Cashier)
                .Where(s => s.StoreId == storeId && s.Cashier.SellerId == sellerId)
                .ToListAsync();

            if (!sessions.Any())
                throw new NotFoundException("Session not found");

            return sessions;
        }
    }
}
