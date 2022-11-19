namespace Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class f : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 15),
                        Address = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Total = c.Decimal(precision: 18, scale: 2),
                        Date = c.DateTime(),
                        Customer_ID = c.Int(),
                        SalesMan_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customers", t => t.Customer_ID)
                .ForeignKey("dbo.SalesMen", t => t.SalesMan_ID)
                .Index(t => t.Customer_ID)
                .Index(t => t.SalesMan_ID);
            
            CreateTable(
                "dbo.SalesMen",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 15),
                        Address = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.InvoiceProducts",
                c => new
                    {
                        InvoiceID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        CountofProduct = c.Int(nullable: false),
                        PriceofTotalProduct = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.InvoiceID, t.ProductID })
                .ForeignKey("dbo.Invoices", t => t.InvoiceID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.InvoiceID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Count = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.PurchaseInvoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(),
                        SalesMan_ID = c.Int(),
                        supplier_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SalesMen", t => t.SalesMan_ID)
                .ForeignKey("dbo.Suppliers", t => t.supplier_ID)
                .Index(t => t.SalesMan_ID)
                .Index(t => t.supplier_ID);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 15),
                        Address = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PurchaseInvoiceProducts",
                c => new
                    {
                        PurchaseInvoiceID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        PurchasingPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CountofProduct = c.Int(nullable: false),
                        PriceofTotalProduct = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.PurchaseInvoiceID, t.ProductID })
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.PurchaseInvoices", t => t.PurchaseInvoiceID, cascadeDelete: true)
                .Index(t => t.PurchaseInvoiceID)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseInvoiceProducts", "PurchaseInvoiceID", "dbo.PurchaseInvoices");
            DropForeignKey("dbo.PurchaseInvoiceProducts", "ProductID", "dbo.Products");
            DropForeignKey("dbo.PurchaseInvoices", "supplier_ID", "dbo.Suppliers");
            DropForeignKey("dbo.PurchaseInvoices", "SalesMan_ID", "dbo.SalesMen");
            DropForeignKey("dbo.InvoiceProducts", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.InvoiceProducts", "InvoiceID", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "SalesMan_ID", "dbo.SalesMen");
            DropForeignKey("dbo.Invoices", "Customer_ID", "dbo.Customers");
            DropIndex("dbo.PurchaseInvoiceProducts", new[] { "ProductID" });
            DropIndex("dbo.PurchaseInvoiceProducts", new[] { "PurchaseInvoiceID" });
            DropIndex("dbo.PurchaseInvoices", new[] { "supplier_ID" });
            DropIndex("dbo.PurchaseInvoices", new[] { "SalesMan_ID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropIndex("dbo.InvoiceProducts", new[] { "ProductID" });
            DropIndex("dbo.InvoiceProducts", new[] { "InvoiceID" });
            DropIndex("dbo.Invoices", new[] { "SalesMan_ID" });
            DropIndex("dbo.Invoices", new[] { "Customer_ID" });
            DropTable("dbo.PurchaseInvoiceProducts");
            DropTable("dbo.Suppliers");
            DropTable("dbo.PurchaseInvoices");
            DropTable("dbo.Products");
            DropTable("dbo.InvoiceProducts");
            DropTable("dbo.SalesMen");
            DropTable("dbo.Invoices");
            DropTable("dbo.Customers");
            DropTable("dbo.Categories");
        }
    }
}
