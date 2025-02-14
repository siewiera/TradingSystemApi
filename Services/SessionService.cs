using AutoMapper;
using TradingSystemApi.Interface.RepositoriesInterface;

namespace TradingSystemApi.Services
{
    public class SessionService
    {
        private readonly IMapper _mapper;
        private readonly IStoreRepository _storeRepository;
        private readonly ISessionRepository _sessionRepository;

        public SessionService(IMapper mapper, IStoreRepository storeRepository, ISessionRepository sessionRepository)
        {
            _mapper = mapper;
            _storeRepository = storeRepository;
            _sessionRepository = sessionRepository;
        }
    }
}
