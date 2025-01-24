using TradingSystemApi.Entities;

namespace TradingSystemApi.Models.BarcodeDto
{
    public class AddNewBarcodeDto
    {
        public string Code { get; set; }
        public bool Active { get; set; }
        public DateTime CreationDate { get; set; }
        //public DateTime UpdateDate { get; set; }

        public int StoreId { get; set; }
    }
}
