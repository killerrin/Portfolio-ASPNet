namespace Portfolio.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserAndRoles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Email = c.String(),
                        PasswordHash = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        User_UserID = c.Int(nullable: false),
                        Role_RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserID, t.Role_RoleID })
                .ForeignKey("dbo.Users", t => t.User_UserID, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_RoleID, cascadeDelete: true)
                .Index(t => t.User_UserID)
                .Index(t => t.Role_RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "Role_RoleID", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "User_UserID", "dbo.Users");
            DropIndex("dbo.UserRoles", new[] { "Role_RoleID" });
            DropIndex("dbo.UserRoles", new[] { "User_UserID" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
        }
    }
}
