using Acctive.Models.Application;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Acctive.Models.Context
{
    public class AcctiveDbContext : DbContext
    {
        #region Constructors

        public AcctiveDbContext() : this("DefaultConnection")
        {
        }

        public AcctiveDbContext(string connectionString) : base(connectionString)
        {
            //Database.SetInitializer<MyDataContext>(null);
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AcctiveDbContext>());
        }

        #endregion Constructors

        #region Application

        public DbSet<AppConfig> AppConfig { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }

        #endregion Application

        #region Company

        public DbSet<Company> Company { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<CompanyConfig> CompanyConfig { get; set; }
        public DbSet<Period> Period { get; set; }

        #endregion Company

        #region Accounting

        public DbSet<Accounting.AccountCategory> AccountCategory { get; set; }
        public DbSet<Accounting.Account> Account { get; set; }
        public DbSet<Accounting.AccountBalance> AccountBalance { get; set; }
        public DbSet<Accounting.AccountBank> AccountBank { get; set; }

        public DbSet<Accounting.Journal> Journal { get; set; }
        public DbSet<Accounting.JournalItem> JournalItem { get; set; }
        public DbSet<Accounting.Transaction> Transaction { get; set; }

        #endregion Accounting

        #region Inventory

        public DbSet<Inventory.Unit> Unit { get; set; }
        public DbSet<Inventory.UnitConversion> UnitConversion { get; set; }
        public DbSet<Inventory.Warehouse> Warehouse { get; set; }
        public DbSet<Inventory.ProductCategory> ProductCategory { get; set; }
        public DbSet<Inventory.Product> Product { get; set; }
        public DbSet<Inventory.Inventory> Inventory { get; set; }
        public DbSet<Inventory.Invoice> Invoice { get; set; }
        public DbSet<Inventory.InvoiceItem> InvoiceItem { get; set; }
        public DbSet<Inventory.InvoiceMisc> InvoiceMisc { get; set; }
        public DbSet<Inventory.InvoicePaymentReceipt> InvoicePaymentReceipt { get; set; }

        #endregion Inventory

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            #region One-to-many and many-to-many

            modelBuilder.Entity<Company>()
                .HasMany(c => c.Addresses)
                .WithMany(a => a.Companies)
                .Map(m =>
                {
                    m.MapLeftKey("CompanyId");
                    m.MapRightKey("AddressId");
                    m.ToTable("CompanyAddress");
                });

            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .Map(m => m.MapLeftKey("UserId").MapRightKey("RoleId").ToTable("UserRole"));

            modelBuilder.Entity<Accounting.Account>()
                .HasMany(a => a.Addresses)
                .WithMany(d => d.Accounts)
                .Map(m => m.MapLeftKey("AccountId").MapRightKey("AddressId").ToTable("AccountAddress"));

            modelBuilder.Entity<Accounting.Journal>()
                .HasMany(j => j.Transactions)
                .WithMany(t => t.Journals)
                .Map(m => m.MapLeftKey("JournalId").MapRightKey("TransactionId").ToTable("JournalTransaction"));

            modelBuilder.Entity<Inventory.Invoice>()
                .HasMany(j => j.Transactions)
                .WithMany(t => t.Invoices)
                .Map(m => m.MapLeftKey("InvoiceId").MapRightKey("TransactionId").ToTable("InvoiceTransaction"));

            modelBuilder.Entity<Inventory.UnitConversion>()
                .Property(x => x.Factor).HasPrecision(12, 5);

            #endregion One-to-many and many-to-many

            #region RowVersion => ConcurrencyToken

            modelBuilder.Entity<Accounting.Journal>()
                .Property(x => x.RowVersion).IsConcurrencyToken();
            modelBuilder.Entity<Inventory.Invoice>()
                .Property(x => x.RowVersion).IsConcurrencyToken();

            #endregion RowVersion => ConcurrencyToken
        }
    }
}