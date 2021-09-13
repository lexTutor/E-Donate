namespace PPaymentService.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePaymentStstusTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PaymentStatus", "PaymentId", "dbo.Payment");
            DropIndex("dbo.PaymentStatus", new[] { "PaymentId" });
            DropTable("dbo.PaymentStatus");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.PaymentStatus", "PaymentId");
            AddForeignKey("dbo.PaymentStatus", "PaymentId", "dbo.Payment", "Id");
        }
    }
}
