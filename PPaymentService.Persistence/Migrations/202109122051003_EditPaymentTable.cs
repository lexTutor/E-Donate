namespace PPaymentService.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditPaymentTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Payment", "CreditCardNumber");
            DropColumn("dbo.Payment", "CardHolder");
            DropColumn("dbo.Payment", "ExpiryDate");
            DropColumn("dbo.Payment", "SecurityCode");
            DropColumn("dbo.Payment", "PaymentSatustId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payment", "PaymentSatustId", c => c.String());
            AddColumn("dbo.Payment", "SecurityCode", c => c.String());
            AddColumn("dbo.Payment", "ExpiryDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Payment", "CardHolder", c => c.String());
            AddColumn("dbo.Payment", "CreditCardNumber", c => c.String());
        }
    }
}
