namespace LibraryManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateBookTypes : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT BOOKTYPES ON");
                Sql("INSERT INTO BOOKTYPES(Id, Type) VALUES (1, 'Adventure')");
                Sql("INSERT INTO BOOKTYPES(Id, Type) VALUES (2, 'Art')");
                Sql("INSERT INTO BOOKTYPES(Id, Type) VALUES (3, 'Classic')");
                Sql("INSERT INTO BOOKTYPES(Id, Type) VALUES (4, 'Dictionary')");
                Sql("INSERT INTO BOOKTYPES(Id, Type) VALUES (5, 'Encyclopedia')");
                Sql("INSERT INTO BOOKTYPES(Id, Type) VALUES (6, 'Fantasy')");
                Sql("INSERT INTO BOOKTYPES(Id, Type) VALUES (7, 'History')");
                Sql("INSERT INTO BOOKTYPES(Id, Type) VALUES (8, 'Horror')");
                Sql("INSERT INTO BOOKTYPES(Id, Type) VALUES (9, 'Political')");
                Sql("INSERT INTO BOOKTYPES(Id, Type) VALUES (10, 'Religion')");
                Sql("INSERT INTO BOOKTYPES(Id, Type) VALUES (11, 'Romance')");
                Sql("INSERT INTO BOOKTYPES(Id, Type) VALUES (12, 'Science Fiction')");
            Sql("SET IDENTITY_INSERT BOOKTYPES OFF");

        }

        public override void Down()
        {
        }
    }
}
