namespace LibraryManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateByAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Books", "Author", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Books", "ISBN", c => c.String(nullable: false, maxLength: 13));
            AlterColumn("dbo.BookTypes", "Type", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Readers", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Readers", "Email", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Readers", "Email", c => c.String());
            AlterColumn("dbo.Readers", "Name", c => c.String());
            AlterColumn("dbo.BookTypes", "Type", c => c.String());
            AlterColumn("dbo.Books", "ISBN", c => c.String());
            AlterColumn("dbo.Books", "Author", c => c.String());
            AlterColumn("dbo.Books", "Name", c => c.String());
        }
    }
}
