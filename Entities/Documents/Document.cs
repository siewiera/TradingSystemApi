using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Entities.BusinessEntities;
using TradingSystemApi.Enum;

namespace TradingSystemApi.Entities.Documents
{
    public abstract class Document
    {
        public int Id { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        [Required]
        public DateTime DateOfIssue { get; set; }
        [Required]
        public DateTime DateOfSale { get; set; }
        [Required]
        public string DocumentNo { get; set; }

        public int BusinessEntityId { get; set; }
        public virtual BusinessEntity BusinessEntity{ get; set; }
        //public int? CustomerId { get; set; }
        //public virtual Customer Customer { get; set; }

        public int StoreId { get; set; }
        public virtual Store Store { get; set; }

        public int CashierId { get; set; }
        public virtual Cashier Cashier { get; set; }

        public virtual InventoryMovement InventoryMovement { get; set; }
        public ICollection<DocumentItem> DocumentItems { get; set; }


        //public DocumentSymbol DocumentSymbol { get; set; }
        //public string DocumentType { get; set; }

    }
}
