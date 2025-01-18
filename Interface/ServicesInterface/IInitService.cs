using TradingSystemApi.Models.InitData;

namespace TradingSystemApi.Interface.ServicesInterface
{
    public interface IInitService
    {
        Task CheckInitDataExists();
        Task<int> InitData(InitDataDto dto);
        Task UpdateAdminAccount(UpdateAdminAccountDto updateAdminDto);
    }
}