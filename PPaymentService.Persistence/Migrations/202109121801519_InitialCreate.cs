namespace PPaymentService.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreditCardNumber = c.String(),
                        CardHolder = c.String(),
                        ExpiryDate = c.DateTime(nullable: false),
                        SecurityCode = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Reference = c.String(),
                        PaymentSatustId = c.String(),
                        Paid_At = c.DateTime(nullable: false),
                        PaymentStatus = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaymentStatus",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Status = c.Int(nullable: false),
                        PaymentId = c.String(maxLength: 128),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Payment", t => t.PaymentId)
                .Index(t => t.PaymentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentStatus", "PaymentId", "dbo.Payment");
            DropIndex("dbo.PaymentStatus", new[] { "PaymentId" });
            DropTable("dbo.PaymentStatus");
            DropTable("dbo.Payment");
        }
    }
}
