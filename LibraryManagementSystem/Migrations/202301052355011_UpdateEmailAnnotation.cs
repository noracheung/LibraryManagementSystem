namespace LibraryManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEmailAnnotation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EmailNotifications", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.EmailNotifications", "BookName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmailNotifications", "BookName", c => c.String());
            AlterColumn("dbo.EmailNotifications", "Email", c => c.String());
        }
    }
}
