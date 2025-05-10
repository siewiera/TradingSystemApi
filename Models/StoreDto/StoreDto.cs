using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Entities;
using TradingSystemApi.Entities.BusinessEntities;
using TradingSystemApi.Enum;


namespace TradingSystemApi.Models.StoreDto
{
    public class StoreDto
    {
        public int Id{ get; set; }
        [Required]
        public string Name { get; set; }
        public bool AdminStore { get; set; }
        public bool GlobalStore { get; set; }

        public virtual ICollection<BusinessEntity> BusinessEntities { get; set; }
        public ICollection<AdressDto.AdressDto> AdressDto { get; set; }
    }
}
