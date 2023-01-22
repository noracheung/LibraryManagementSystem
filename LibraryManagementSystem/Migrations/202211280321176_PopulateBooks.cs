namespace LibraryManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateBooks : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT BOOKS ON");
            Sql("INSERT INTO BOOKS(Id, Name, Author, ISBN, NumberInStock, BookTypeId) VALUES (1, 'Dune', 'Frank Herbert', 9780425266540, 10, 12)");
            Sql("INSERT INTO BOOKS(Id, Name, Author, ISBN, NumberInStock, BookTypeId) VALUES (2, 'Foundation', 'Isaac Asimov', 9780553293357, 15, 12)");
            Sql("INSERT INTO BOOKS(Id, Name, Author, ISBN, NumberInStock, BookTypeId) VALUES (3, 'Arrival', 'Ted Chiang', 9781509835904, 2, 12)");
            Sql("INSERT INTO BOOKS(Id, Name, Author, ISBN, NumberInStock, BookTypeId) VALUES (4, 'Little women', 'Louisa May Alcott', 9780140390698, 12, 3)");
            Sql("INSERT INTO BOOKS(Id, Name, Author, ISBN, NumberInStock, BookTypeId) VALUES (5, 'Les Misérables', 'Victor Hugo', 9780486457895, 0, 3)");
            Sql("INSERT INTO BOOKS(Id, Name, Author, ISBN, NumberInStock, BookTypeId) VALUES (6, 'Life of Pi', 'Yann Martel', 9781782118695, 1, 1)");
            Sql("INSERT INTO BOOKS(Id, Name, Author, ISBN, NumberInStock, BookTypeId) VALUES (7, 'A Game of Thrones', 'George R. R. Martin', 9780007428540, 22, 6)");
            Sql("INSERT INTO BOOKS(Id, Name, Author, ISBN, NumberInStock, BookTypeId) VALUES (8, 'The Stand', 'Stephen King', 9780307743688, 4, 8)");
            Sql("INSERT INTO BOOKS(Id, Name, Author, ISBN, NumberInStock, BookTypeId) VALUES (9, 'In Gods We Trust - The Evolutionary Landscape of Religion', 'Scott Atran', 9780195178036, 10, 7)");
            Sql("INSERT INTO BOOKS(Id, Name, Author, ISBN, NumberInStock, BookTypeId) VALUES (10, 'The Sociology of Religion', 'Grace Davie', 9781446238851, 2, 10)");
            Sql("INSERT INTO BOOKS(Id, Name, Author, ISBN, NumberInStock, BookTypeId) VALUES (11, 'Justice and the Politics of Difference', 'Iris Marion Young, Danielle S. Allen', 9780691152622, 0, 9)");
            Sql("INSERT INTO BOOKS(Id, Name, Author, ISBN, NumberInStock, BookTypeId) VALUES (12, 'Dear John', 'Nicholas Sparks', 9780759568990, 7, 11)");
            Sql("INSERT INTO BOOKS(Id, Name, Author, ISBN, NumberInStock, BookTypeId) VALUES (13, 'Super Mario Encyclopedia', 'Nintendo', 9781506708973, 7, 5)");
            Sql("SET IDENTITY_INSERT BOOKS OFF");
        }
        
        public override void Down()
        {
        }
    }
}
