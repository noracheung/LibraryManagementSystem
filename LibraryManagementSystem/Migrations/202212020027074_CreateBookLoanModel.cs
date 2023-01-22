namespace LibraryManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateBookLoanModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookLoans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReaderId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        BorrowDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Readers", t => t.ReaderId, cascadeDelete: true)
                .Index(t => t.ReaderId)
                .Index(t => t.BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookLoans", "ReaderId", "dbo.Readers");
            DropForeignKey("dbo.BookLoans", "BookId", "dbo.Books");
            DropIndex("dbo.BookLoans", new[] { "BookId" });
            DropIndex("dbo.BookLoans", new[] { "ReaderId" });
            DropTable("dbo.BookLoans");
        }
    }
}
