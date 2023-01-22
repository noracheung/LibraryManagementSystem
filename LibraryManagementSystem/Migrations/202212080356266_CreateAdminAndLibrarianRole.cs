namespace LibraryManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAdminAndLibrarianRole : DbMigration
    {
        public override void Up()
        {
            Sql(@"
            INSERT INTO[dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES(N'32a1bc07-1d5b-4421-8d2c-82d7b1d5dd57', N'librarian@gmail.com', 0, N'AHZHxhYyY/+mSW0AVDD6bcK4bKcGz6SQ4gCy4kSZefy53lB0Yz0HvnwQXBB4dV5KpA==', N'75fcf126-7c94-4273-bfce-cc1cbadb774f', NULL, 0, 0, NULL, 1, 0, N'librarian@gmail.com')
            INSERT INTO[dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES(N'720b16d1-891e-4259-be3d-971008563a50', N'admin@gmail.com', 0, N'AMMygfEL6lRhG65NLYBrYQ991JcYVFbw6CrZDCSL/3RykgpPHrVy8MNmKvlMPaGlig==', N'9a1b21c1-fe24-4159-92f2-d156d27e2b6f', NULL, 0, 0, NULL, 1, 0, N'admin@gmail.com')
            INSERT INTO[dbo].[AspNetRoles] ([Id], [Name]) VALUES(N'32a1bc07-1d5b-4421-8d2c-82d7b1d5dd57', N'Librarian')
            INSERT INTO[dbo].[AspNetRoles] ([Id], [Name]) VALUES(N'720b16d1-891e-4259-be3d-971008563a50', N'Admin')
            ");

        }

        public override void Down()
        {
        }
    }
}
