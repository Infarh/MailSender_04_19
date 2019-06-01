namespace MailSender.lib.Data.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecipientAddDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipients", "Description", c => c.String());

            //Sql("");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recipients", "Description");
            //Sql("");
        }
    }
}
