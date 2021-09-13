namespace PPaymentService.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateConfigForPayment : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Payment", "Paid_At", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Payment", "Paid_At", c => c.DateTime(nullable: false));
        }
    }
}
