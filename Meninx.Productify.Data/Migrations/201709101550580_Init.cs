namespace Meninx.Productify.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AttributeTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AttributeTypes", t => t.AttributeTypeId, cascadeDelete: true)
                .Index(t => t.AttributeTypeId);
            
            CreateTable(
                "dbo.AttributeTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DataType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductAttributes",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        Attribute_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.Attribute_Id })
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .ForeignKey("dbo.Attributes", t => t.Attribute_Id, cascadeDelete: true)
                .Index(t => t.Product_Id)
                .Index(t => t.Attribute_Id);
            
            CreateTable(
                "dbo.DatetimeAtrribute",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        DatetimeValue = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Attributes", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.IntegerAtrribute",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        IntegerValue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Attributes", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.StringAtrribute",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        StringValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Attributes", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StringAtrribute", "Id", "dbo.Attributes");
            DropForeignKey("dbo.IntegerAtrribute", "Id", "dbo.Attributes");
            DropForeignKey("dbo.DatetimeAtrribute", "Id", "dbo.Attributes");
            DropForeignKey("dbo.ProductAttributes", "Attribute_Id", "dbo.Attributes");
            DropForeignKey("dbo.ProductAttributes", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Attributes", "AttributeTypeId", "dbo.AttributeTypes");
            DropIndex("dbo.StringAtrribute", new[] { "Id" });
            DropIndex("dbo.IntegerAtrribute", new[] { "Id" });
            DropIndex("dbo.DatetimeAtrribute", new[] { "Id" });
            DropIndex("dbo.ProductAttributes", new[] { "Attribute_Id" });
            DropIndex("dbo.ProductAttributes", new[] { "Product_Id" });
            DropIndex("dbo.Attributes", new[] { "AttributeTypeId" });
            DropTable("dbo.StringAtrribute");
            DropTable("dbo.IntegerAtrribute");
            DropTable("dbo.DatetimeAtrribute");
            DropTable("dbo.ProductAttributes");
            DropTable("dbo.Products");
            DropTable("dbo.AttributeTypes");
            DropTable("dbo.Attributes");
        }
    }
}
