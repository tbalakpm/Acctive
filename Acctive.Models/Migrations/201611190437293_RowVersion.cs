namespace Acctive.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RowVersion : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "CostPrice", c => c.Decimal(storeType: "money"));
            AlterColumn("dbo.Product", "ProfitPercent", c => c.Decimal(storeType: "money"));
            AlterColumn("dbo.Product", "SellingPrice", c => c.Decimal(storeType: "money"));
            AlterColumn("dbo.Product", "TaxPercent", c => c.Decimal(storeType: "money"));
            AlterColumn("dbo.Product", "Surcharge", c => c.Decimal(storeType: "money"));
            AlterColumn("dbo.Product", "Freight", c => c.Decimal(storeType: "money"));
            AlterColumn("dbo.Product", "MinimumQuantity", c => c.Decimal(storeType: "money"));
            AlterColumn("dbo.Product", "MaximumQuantity", c => c.Decimal(storeType: "money"));
            AlterColumn("dbo.Product", "ReorderLevelQuantity", c => c.Decimal(storeType: "money"));
            DropColumn("dbo.CompanyConfig", "RowVersion");
            DropColumn("dbo.AppConfig", "RowVersion");
            DropColumn("dbo.Product", "LevelNumber");
            DropColumn("dbo.Product", "IndexNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "IndexNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "LevelNumber", c => c.Int(nullable: false));
            AddColumn("dbo.AppConfig", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.CompanyConfig", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AlterColumn("dbo.Product", "ReorderLevelQuantity", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.Product", "MaximumQuantity", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.Product", "MinimumQuantity", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.Product", "Freight", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.Product", "Surcharge", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.Product", "TaxPercent", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.Product", "SellingPrice", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.Product", "ProfitPercent", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.Product", "CostPrice", c => c.Decimal(nullable: false, storeType: "money"));
        }
    }
}
