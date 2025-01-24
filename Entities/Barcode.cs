namespace TradingSystemApi.Entities
{
    public class Barcode
    {
        public int Id{ get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public int StoreId { get; set; }
        public virtual Store Store { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
