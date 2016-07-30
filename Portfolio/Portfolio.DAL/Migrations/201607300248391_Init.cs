namespace Portfolio.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Slug = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.PortfolioEntryCategories",
                c => new
                    {
                        PortfolioEntryCategoryId = c.Int(nullable: false, identity: true),
                        PortfolioEntryId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PortfolioEntryCategoryId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.PortfolioEntries", t => t.PortfolioEntryId, cascadeDelete: true)
                .Index(t => t.PortfolioEntryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.PortfolioEntries",
                c => new
                    {
                        PortfolioEntryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Slug = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UnpublishedAt = c.DateTime(),
                        CoverImageUrl = c.String(),
                        VideoEmbedUrl = c.String(),
                        SourceControlUrl = c.String(),
                        EmbedUrl = c.String(),
                        Description = c.String(),
                        Features = c.String(),
                        Screenshots = c.String(),
                        WebsiteUrl = c.String(),
                        GooglePlayStoreUrl = c.String(),
                        AppleAppStoreUrl = c.String(),
                        MicrosoftWindowsStoreUrl = c.String(),
                        OtherMarketplaceUrls = c.String(),
                    })
                .PrimaryKey(t => t.PortfolioEntryId);
            
            CreateTable(
                "dbo.PortfolioEntryFrameworks",
                c => new
                    {
                        PortfolioEntryFrameworkId = c.Int(nullable: false, identity: true),
                        PortfolioEntryId = c.Int(nullable: false),
                        FrameworkId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PortfolioEntryFrameworkId)
                .ForeignKey("dbo.Frameworks", t => t.FrameworkId, cascadeDelete: true)
                .ForeignKey("dbo.PortfolioEntries", t => t.PortfolioEntryId, cascadeDelete: true)
                .Index(t => t.PortfolioEntryId)
                .Index(t => t.FrameworkId);
            
            CreateTable(
                "dbo.Frameworks",
                c => new
                    {
                        FrameworkId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Slug = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.FrameworkId);
            
            CreateTable(
                "dbo.PortfolioEntryPlatforms",
                c => new
                    {
                        PortfolioEntryPlatformId = c.Int(nullable: false, identity: true),
                        PortfolioEntryId = c.Int(nullable: false),
                        PlatformId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PortfolioEntryPlatformId)
                .ForeignKey("dbo.Platforms", t => t.PlatformId, cascadeDelete: true)
                .ForeignKey("dbo.PortfolioEntries", t => t.PortfolioEntryId, cascadeDelete: true)
                .Index(t => t.PortfolioEntryId)
                .Index(t => t.PlatformId);
            
            CreateTable(
                "dbo.Platforms",
                c => new
                    {
                        PlatformId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Slug = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PlatformId);
            
            CreateTable(
                "dbo.PortfolioEntryProgrammingLanguages",
                c => new
                    {
                        PortfolioEntryProgrammingLanguageId = c.Int(nullable: false, identity: true),
                        PortfolioEntryId = c.Int(nullable: false),
                        ProgrammingLanguageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PortfolioEntryProgrammingLanguageId)
                .ForeignKey("dbo.PortfolioEntries", t => t.PortfolioEntryId, cascadeDelete: true)
                .ForeignKey("dbo.ProgrammingLanguages", t => t.ProgrammingLanguageId, cascadeDelete: true)
                .Index(t => t.PortfolioEntryId)
                .Index(t => t.ProgrammingLanguageId);
            
            CreateTable(
                "dbo.ProgrammingLanguages",
                c => new
                    {
                        ProgrammingLanguageId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Slug = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ProgrammingLanguageId);
            
            CreateTable(
                "dbo.PortfolioEntryTags",
                c => new
                    {
                        PortfolioEntryTagId = c.Int(nullable: false, identity: true),
                        PortfolioEntryId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PortfolioEntryTagId)
                .ForeignKey("dbo.PortfolioEntries", t => t.PortfolioEntryId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.PortfolioEntryId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Slug = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Email = c.String(),
                        PasswordHash = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        User_UserId = c.Int(nullable: false),
                        Role_RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserId, t.Role_RoleId })
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_RoleId, cascadeDelete: true)
                .Index(t => t.User_UserId)
                .Index(t => t.Role_RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "Role_RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.PortfolioEntryTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.PortfolioEntryTags", "PortfolioEntryId", "dbo.PortfolioEntries");
            DropForeignKey("dbo.PortfolioEntryProgrammingLanguages", "ProgrammingLanguageId", "dbo.ProgrammingLanguages");
            DropForeignKey("dbo.PortfolioEntryProgrammingLanguages", "PortfolioEntryId", "dbo.PortfolioEntries");
            DropForeignKey("dbo.PortfolioEntryPlatforms", "PortfolioEntryId", "dbo.PortfolioEntries");
            DropForeignKey("dbo.PortfolioEntryPlatforms", "PlatformId", "dbo.Platforms");
            DropForeignKey("dbo.PortfolioEntryFrameworks", "PortfolioEntryId", "dbo.PortfolioEntries");
            DropForeignKey("dbo.PortfolioEntryFrameworks", "FrameworkId", "dbo.Frameworks");
            DropForeignKey("dbo.PortfolioEntryCategories", "PortfolioEntryId", "dbo.PortfolioEntries");
            DropForeignKey("dbo.PortfolioEntryCategories", "CategoryId", "dbo.Categories");
            DropIndex("dbo.UserRoles", new[] { "Role_RoleId" });
            DropIndex("dbo.UserRoles", new[] { "User_UserId" });
            DropIndex("dbo.PortfolioEntryTags", new[] { "TagId" });
            DropIndex("dbo.PortfolioEntryTags", new[] { "PortfolioEntryId" });
            DropIndex("dbo.PortfolioEntryProgrammingLanguages", new[] { "ProgrammingLanguageId" });
            DropIndex("dbo.PortfolioEntryProgrammingLanguages", new[] { "PortfolioEntryId" });
            DropIndex("dbo.PortfolioEntryPlatforms", new[] { "PlatformId" });
            DropIndex("dbo.PortfolioEntryPlatforms", new[] { "PortfolioEntryId" });
            DropIndex("dbo.PortfolioEntryFrameworks", new[] { "FrameworkId" });
            DropIndex("dbo.PortfolioEntryFrameworks", new[] { "PortfolioEntryId" });
            DropIndex("dbo.PortfolioEntryCategories", new[] { "CategoryId" });
            DropIndex("dbo.PortfolioEntryCategories", new[] { "PortfolioEntryId" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Tags");
            DropTable("dbo.PortfolioEntryTags");
            DropTable("dbo.ProgrammingLanguages");
            DropTable("dbo.PortfolioEntryProgrammingLanguages");
            DropTable("dbo.Platforms");
            DropTable("dbo.PortfolioEntryPlatforms");
            DropTable("dbo.Frameworks");
            DropTable("dbo.PortfolioEntryFrameworks");
            DropTable("dbo.PortfolioEntries");
            DropTable("dbo.PortfolioEntryCategories");
            DropTable("dbo.Categories");
        }
    }
}
