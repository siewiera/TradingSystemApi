namespace TradingSystemApi.Models.BarcodeDto
{
    public class UpdateBarcodeDataDto
    {
        public string Code { get; set; }
        public bool Active { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
