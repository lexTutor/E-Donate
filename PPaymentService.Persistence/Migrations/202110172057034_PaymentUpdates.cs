namespace PPaymentService.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentUpdates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payment", "Email", c => c.String());
            AddColumn("dbo.Payment", "PaymentMethod", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payment", "PaymentMethod");
            DropColumn("dbo.Payment", "Email");
        }
    }
}
