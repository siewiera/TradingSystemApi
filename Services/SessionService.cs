using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json;
using TradingSystemApi.Entities;
using TradingSystemApi.Interface.RepositoriesInterface;
using TradingSystemApi.Interface.ServicesInterface;
using TradingSystemApi.Models.CashierDto;
using TradingSystemApi.Models.Session;

namespace TradingSystemApi.Services
{
    public class SessionService : ISessionService
    {
        private readonly IMapper _mapper;
        private readonly IStoreRepository _storeRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly ICashierRepository _cashierRepository;
        private readonly ISellerRepository _sellerRepository;
        private string _configFilePath = Path.Combine("Settings", "sessionSettings.json");
        private SessionSettings _time;
        private readonly TimeSpan _sessionTimeout;

        public SessionService(IMapper mapper, IStoreRepository storeRepository, ISessionRepository sessionRepository, ICashierRepository cashierRepository, ISellerRepository sellerRepository)
        {
            _mapper = mapper;
            _storeRepository = storeRepository;
            _sessionRepository = sessionRepository;
            _cashierRepository = cashierRepository;
            _sellerRepository = sellerRepository;
            _time = LoadingSessionSettingsFile();
            _sessionTimeout = TimeSpan.FromSeconds(_time.sessionTime);
        }

        public SessionSettings LoadingSessionSettingsFile()
        {
            try
            {
                var jsonConfig = File.ReadAllText(_configFilePath);
                using JsonDocument doc = JsonDocument.Parse(jsonConfig);
                var sessionConfig = doc.RootElement.GetProperty("Session").GetRawText();
                
                return JsonConvert.DeserializeObject<SessionSettings>(sessionConfig);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task CheckData(int storeId, int sellerId, int cashierId = -1)
        {
            await _storeRepository.CheckStoreById(storeId);
            await _sellerRepository.CheckSellerById(storeId, sellerId);
            if (cashierId > -1)
                await _cashierRepository.GetCashierDataById(storeId, sellerId, cashierId);
        }

        /**/

        public async Task<int> AddNewSession(int storeId, int sellerId, int cashierId, string ip)
        {
            await CheckData(storeId, sellerId, cashierId);
            await _cashierRepository.CheckCashierAccount(storeId, sellerId, cashierId);
            await _sessionRepository.CheckSessionExistsByCashierId(storeId, sellerId, cashierId);

            var session = new Session()
            {
                SessionGuid = Guid.NewGuid(),
                LoginTime = DateTime.Now,
                LastAction = DateTime.Now,
                Ip = ip,
                CashierId = cashierId,
                StoreId = storeId
            };

            await _sessionRepository.AddNewSession(session, storeId);
            return session.Id;
        }

        public async Task ExtensionSessionByCashierId(int storeId, int sellerId, int cashierId)
        {
            await CheckData(storeId, sellerId, cashierId);
            await _cashierRepository.CheckCashierAccount(storeId, sellerId, cashierId);

            var session = await _sessionRepository.GetSessionDataByCashierId(storeId, sellerId, cashierId);
            //dto.LastAction = DateTime.Now;
            session.LastAction = DateTime.Now;

            await _sessionRepository.UpdateSessionData(session);
        }

        public async Task ExtensionSessionBySessionGuid(int storeId, int sellerId, Guid sessionGuid)
        {
            await CheckData(storeId, sellerId, -1);

            var session = await _sessionRepository.GetSessionDataBySessionGuid(storeId, sellerId, sessionGuid);
            await _cashierRepository.CheckCashierAccount(storeId, sellerId, session.CashierId);
            session.LastAction = DateTime.Now;

            await _sessionRepository.UpdateSessionData(session);
        }

        public async Task DeleteSessionByCashierid(int storeId, int sellerId, int cashierId)
        {
            await CheckData(storeId, sellerId, cashierId);
            var session = await _sessionRepository.GetSessionDataByCashierId(storeId, sellerId, cashierId);
            await _sessionRepository.DeleteSession(session);
        }

        public async Task<SessionDto> GetSessionDataByCashierId(int storeId, int sellerId, int cashierId)
        {
            await CheckData(storeId, sellerId, cashierId);
            var session = await _sessionRepository.GetSessionDataByCashierId(storeId, sellerId, cashierId);
            var sessionDto = _mapper.Map<SessionDto>(session);
            return sessionDto;
        }

        public async Task<SessionDto> GetSessionDataBySessionGuid(int storeId, int sellerId, Guid sessionGuid)
        {
            await CheckData(storeId, sellerId, -1);
            var session = await _sessionRepository.GetSessionDataBySessionGuid(storeId, sellerId, sessionGuid);
            var sessionDto = _mapper.Map<SessionDto>(session);
            return sessionDto;
        }

        public async Task<IEnumerable<SessionDto>> GetAllSessionsData(int storeId, int sellerId)
        {
            await CheckData(storeId, sellerId, -1);
            var sessions = await _sessionRepository.GetAllSessionsData(storeId, sellerId);
            var sessionsDto = _mapper.Map<IEnumerable<SessionDto>>(sessions);

            return sessionsDto;
        }


        public async Task RemoveInactiveSession()
        {
            var thresholdTime = DateTime.Now - _sessionTimeout;
            await _sessionRepository.RemoveInactiveSession(thresholdTime);
        }
    }
}
