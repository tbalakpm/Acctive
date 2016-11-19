namespace Acctive.Models.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Context.AcctiveDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Context.AcctiveDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.AccountCategory.AddOrUpdate(
                c => c.Code,
                new Accounting.AccountCategory { Code = "INCOME", Name = "Income", Description = "Money received, especially on a regular basis, for work or through investments." },
                new Accounting.AccountCategory { Code = "EXPENSE", Name = "Expense", Description = "The cost incurred in or required for something." },
                new Accounting.AccountCategory { Code = "ASSET", Name = "Asset" },
                new Accounting.AccountCategory { Code = "LIABILITY", Name = "Liability" },
                new Accounting.AccountCategory { Code = "CASH", Name = "Cash" },
                new Accounting.AccountCategory { Code = "BANK", Name = "Bank" },
                new Accounting.AccountCategory { Code = "CUSTOMER", Name = "Customer" },
                new Accounting.AccountCategory { Code = "SUPPLIER", Name = "Supplier" },
                new Accounting.AccountCategory { Code = "PURCHASE", Name = "Purchase" },
                new Accounting.AccountCategory { Code = "PUR_RET", Name = "Purchase Return" },
                new Accounting.AccountCategory { Code = "SALES", Name = "Sales" },
                new Accounting.AccountCategory { Code = "SAL_RET", Name = "Sales Return" },
                new Accounting.AccountCategory { Code = "STOCK", Name = "Opening Stock" },
                new Accounting.AccountCategory { Code = "RAW_MAT", Name = "Raw Material" },
                new Accounting.AccountCategory { Code = "BRANCH", Name = "Branch" }
                );
        }
    }
}