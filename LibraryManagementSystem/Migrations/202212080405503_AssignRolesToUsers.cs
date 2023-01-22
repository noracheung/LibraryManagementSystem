namespace LibraryManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssignRolesToUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO[dbo].[AspNetRoles] ([Id], [Name]) VALUES(N'1', N'Librarian')
                INSERT INTO[dbo].[AspNetRoles] ([Id], [Name]) VALUES(N'2', N'Admin')
                
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'32a1bc07-1d5b-4421-8d2c-82d7b1d5dd57', N'1')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'720b16d1-891e-4259-be3d-971008563a50', N'2')

                ");
        }
        
        public override void Down()
        {
        }
    }
}
