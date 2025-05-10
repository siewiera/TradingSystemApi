using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using TradingSystemApi.Entities;
using TradingSystemApi.Entities.BusinessEntities;
using TradingSystemApi.Entities.BusinessEntities.Customer;
using TradingSystemApi.Entities.BusinessEntities.Seller;
using TradingSystemApi.Entities.Documents;
using TradingSystemApi.Entities.Documents.Invoice.Purchase;
using TradingSystemApi.Entities.Documents.Invoice.Sales;
using TradingSystemApi.Entities.Documents.Receipts;
using TradingSystemApi.Enum;
using TradingSystemApi.Models;

namespace TradingSystemApi.Context
{
    public class TradingSystemDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public TradingSystemDbContext(DbContextOptions<TradingSystemDbContext> option, IConfiguration configuration) : base(option)
        {
            _configuration = configuration;
        }

        public DbSet<Store> Stores { get; set; }
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<BusinessEntity> BusinessEntities { get; set; }
        public DbSet<Cashier> Cashiers { get; set; }
        //public DbSet<Customer> Customers { get; set; }
        public DbSet<InventoryMovement> InventoryMovements { get; set; }
        public DbSet<InventoryMovementDetail> InventoryMovementDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Document> SalesDocuments { get; set; }
        public DbSet<DocumentItem> SalesDocumentItems { get; set; }
        //public DbSet<Seller> Sellers { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Barcode> Barcodes { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cashier>()
                .Property(c => c.CreatedAt)
                .HasColumnType("datetime2(0)");

            modelBuilder.Entity<InventoryMovement>()
                .Property(im => im.CreationDate)
                .HasColumnType("datetime2(0)");

            modelBuilder.Entity<Product>(p =>
            {
                p.Property(pr => pr.CreationDate)
                .HasColumnType("datetime2(0)");

                p.Property(pr => pr.UpdateDate)
                .HasColumnType("datetime2(0)");
            });

            modelBuilder.Entity<Document>(s => 
            {
                s.Property(sd => sd.DateOfIssue)
                .HasColumnType("datetime2(0)");

                s.Property(sd => sd.DateOfSale)
                .HasColumnType("datetime2(0)");
            });

            modelBuilder.Entity<Session>(se =>
            {
                se.Property(s => s.LoginTime)
                .HasColumnType("datetime2(0)");

                se.Property(s => s.LastAction)
                .HasColumnType("datetime2(0)");
            });

            modelBuilder.Entity<Barcode>(b =>
            {
                b.Property(bc => bc.CreationDate)
                .HasColumnType("datetime2(0)");

                b.Property(bc => bc.UpdateDate)
                .HasColumnType("datetime2(0)");
            });

            //#########################################

            modelBuilder.Entity<Store>(s => 
            {
                s.HasMany(st => st.Adresses)
                .WithOne(a => a.Store)
                .HasForeignKey(a => a.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

                s.HasMany(st => st.Sellers)
                .WithOne(se => se.Store)
                .HasForeignKey(se => se.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

                s.HasMany(st => st.Products)
                .WithOne(p => p.Store)
                .HasForeignKey(p => p.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

                s.HasMany(st => st.Barcodes)
                .WithOne(p => p.Store)
                .HasForeignKey(p => p.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

                s.HasMany(st => st.ProductCategories)
                .WithOne(p => p.Store)
                .HasForeignKey(p => p.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

                s.HasMany(st => st.BusinessEntities)
                .WithOne(c => c.Store)
                .HasForeignKey(c => c.StoreId)
                .OnDelete(DeleteBehavior.Restrict);
                //s.HasMany(st => st.Customers)
                //.WithOne(c => c.Store)
                //.HasForeignKey(c => c.StoreId)
                //.OnDelete(DeleteBehavior.Restrict);

                s.HasMany(st => st.SalesDocuments)
                .WithOne(sd => sd.Store)
                .HasForeignKey(sd => sd.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

                s.HasMany(st => st.InventoryMovements)
                .WithOne(im => im.Store)
                .HasForeignKey(im => im.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

                s.HasMany(st => st.Sessions)
                .WithOne(s => s.Store)
                .HasForeignKey(im => im.StoreId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Seller>()
                .HasOne(se => se.Adress)
                .WithOne(a => a.Seller)
                .HasForeignKey<Seller>(se => se.AdressId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>()
                .HasOne(se => se.Adress)
                .WithOne(a => a.Customer)
                .HasForeignKey<Customer>(se => se.AdressId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Document>()
                .HasDiscriminator<string>("DocumentType")
                .HasValue<SaleInvoice>(DocumentType.SalesInvoice.ToString())
                .HasValue<ReceiptSale>(DocumentType.Receipt.ToString())
                .HasValue<SupplyInvoice>(DocumentType.DeliveryInvoice.ToString());

            modelBuilder.Entity<Document>()
                .HasMany(s => s.DocumentItems)
                .WithOne(si => si.Document)
                .HasForeignKey(si => si.DocumentId)
                .OnDelete(DeleteBehavior.Restrict);
            //.OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Document>()
                .HasOne(sd => sd.InventoryMovement)
                .WithOne(im => im.Document)
                .HasForeignKey<InventoryMovement>(im => im.DocumentId)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<DocumentItem>()
            //    .HasOne(sd => sd.InventoryMovementDetail)
            //    .WithOne(im => im.SalesDocumentItem)
            //    .HasForeignKey<InventoryMovementDetail>(im => im.SalesDocumentItemId)
            //    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Document>()
                .HasOne(sd => sd.Cashier)
                .WithMany(c => c.SalesDocuments)
                .HasForeignKey(sd => sd.CashierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReceiptSale>()
                .HasBaseType<Document>();

            modelBuilder.Entity<SaleInvoice>()
                .HasBaseType<Document>();

            modelBuilder.Entity<SupplyInvoice>()
                .HasBaseType<Document>();

            modelBuilder.Entity<ReceiptSaleItem>()
                .HasBaseType<DocumentItem>();

            modelBuilder.Entity<SaleInvoiceItem>()
                .HasBaseType<DocumentItem>();

            modelBuilder.Entity<SupplyInvoiceItem>()
                .HasBaseType<DocumentItem>();

            //modelBuilder.Entity<InvoiceSale>()
            //    .HasMany(i => i.InvoiceSaleItems)
            //    .WithOne(ii => ii.InvoiceSale)
            //    .HasForeignKey(ii => ii.InvoiceSaleId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<SupplyInvoice>()
            //    .HasMany(s => s.SupplyInvoiceItems)
            //    .WithOne(si => si.SupplyInvoice)
            //    .HasForeignKey(si => si.SupplyInvoiceId)
            //.OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InventoryMovement>()
                .HasMany(im => im.InventoryMovementDetails)
                .WithOne(d => d.InventoryMovement)
                .HasForeignKey(d => d.InventoryMovementId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductCategory)
                .WithMany(pc => pc.Products)
                .HasForeignKey(p => p.ProductCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
               .HasMany(p => p.Barcodes)
               .WithOne(b => b.Product)
               .HasForeignKey(b => b.ProductId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cashier>()
               .HasOne(c => c.Session)
               .WithOne(s => s.Cashier)
               .HasForeignKey<Session>(s => s.CashierId)
               .OnDelete(DeleteBehavior.Restrict);

            //enum conversion
            modelBuilder.Entity<Cashier>()
                .Property(c => c.UserRole)
                .HasConversion<string>();

            modelBuilder.Entity<InventoryMovement>()
                .Property(c => c.InventoryMovementType)
                .HasConversion<string>();

            modelBuilder.Entity<Product>()
                .Property(c => c.JM)
                .HasConversion<string>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string[] connectionType = { "PrivateConnection", "BusinessConnection" };
                var connectionString = _configuration.GetConnectionString(connectionType[1]);

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
