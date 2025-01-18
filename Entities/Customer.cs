using System.ComponentModel.DataAnnotations;

namespace TradingSystemApi.Entities
{
    public class Customer
    {
        public int Id  { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string TaxId { get; set; }

        public int StoreId { get; set; }
        public virtual Store Store { get; set; }

        public int AdressId { get; set; }
        public virtual Adress Adress  { get; set; }

        public virtual ICollection<SalesDocument> SalesDocuments { get; set; }
    }
}
