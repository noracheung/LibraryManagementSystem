namespace LibraryManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateReaders : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "ISBN", c => c.String());

            Sql("SET IDENTITY_INSERT READERS ON");
            Sql("INSERT INTO READERS(Id, Name, Email, Birthdate) VALUES (2, 'James Lee', 'james@gmail.com', '2001-01-01 00:00:00')");
            Sql("INSERT INTO READERS(Id, Name, Email, Birthdate) VALUES (3, 'Lily Parker', 'lily123@gmail.com', '1999-02-21 00:00:00')");
            Sql("INSERT INTO READERS(Id, Name, Email, Birthdate) VALUES (4, 'Charlie Evans', 'charlieeee@gmail.com', '1995-11-11 00:00:00')");
            Sql("INSERT INTO READERS(Id, Name, Email, Birthdate) VALUES (5, 'Mary Smith', 'ms@gmail.com', '1989-06-04 00:00:00')");
            Sql("SET IDENTITY_INSERT READERS OFF");
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "ISBN", c => c.Int(nullable: false));
        }
    }
}
