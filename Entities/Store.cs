using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Entities.BusinessEntities;
using TradingSystemApi.Entities.BusinessEntities.Customer;
using TradingSystemApi.Entities.BusinessEntities.Seller;
using TradingSystemApi.Entities.Documents;
using TradingSystemApi.Models;

namespace TradingSystemApi.Entities
{
    public class Store
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool AdminStore { get; set; } = false;
        public bool GlobalStore { get; set; } = false;


        public virtual ICollection<Adress> Adresses { get; set; }
        public virtual ICollection<BusinessEntity> BusinessEntities { get; set; }
        //public virtual ICollection<Seller> Sellers { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Barcode> Barcodes { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        //public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Document> SalesDocuments { get; set; }
        public virtual ICollection<InventoryMovement> InventoryMovements{ get; set; }
        public virtual ICollection<Session> Sessions { get; set; }

    }
}
