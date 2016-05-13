namespace Portfolio.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PortfolioCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Slug = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.PortfolioEntries",
                c => new
                    {
                        PortfolioEntryID = c.Int(nullable: false, identity: true),
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
                .PrimaryKey(t => t.PortfolioEntryID);
            
            CreateTable(
                "dbo.Frameworks",
                c => new
                    {
                        FrameworkID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Slug = c.String(),
                    })
                .PrimaryKey(t => t.FrameworkID);
            
            CreateTable(
                "dbo.Platforms",
                c => new
                    {
                        PlatformID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Slug = c.String(),
                    })
                .PrimaryKey(t => t.PlatformID);
            
            CreateTable(
                "dbo.ProgrammingLanguages",
                c => new
                    {
                        ProgrammingLanguageID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Slug = c.String(),
                    })
                .PrimaryKey(t => t.ProgrammingLanguageID);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Slug = c.String(),
                    })
                .PrimaryKey(t => t.TagID);
            
            CreateTable(
                "dbo.PortfolioEntryCategories",
                c => new
                    {
                        PortfolioEntry_PortfolioEntryID = c.Int(nullable: false),
                        Category_CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PortfolioEntry_PortfolioEntryID, t.Category_CategoryID })
                .ForeignKey("dbo.PortfolioEntries", t => t.PortfolioEntry_PortfolioEntryID, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryID, cascadeDelete: true)
                .Index(t => t.PortfolioEntry_PortfolioEntryID)
                .Index(t => t.Category_CategoryID);
            
            CreateTable(
                "dbo.FrameworkPortfolioEntries",
                c => new
                    {
                        Framework_FrameworkID = c.Int(nullable: false),
                        PortfolioEntry_PortfolioEntryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Framework_FrameworkID, t.PortfolioEntry_PortfolioEntryID })
                .ForeignKey("dbo.Frameworks", t => t.Framework_FrameworkID, cascadeDelete: true)
                .ForeignKey("dbo.PortfolioEntries", t => t.PortfolioEntry_PortfolioEntryID, cascadeDelete: true)
                .Index(t => t.Framework_FrameworkID)
                .Index(t => t.PortfolioEntry_PortfolioEntryID);
            
            CreateTable(
                "dbo.PlatformPortfolioEntries",
                c => new
                    {
                        Platform_PlatformID = c.Int(nullable: false),
                        PortfolioEntry_PortfolioEntryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Platform_PlatformID, t.PortfolioEntry_PortfolioEntryID })
                .ForeignKey("dbo.Platforms", t => t.Platform_PlatformID, cascadeDelete: true)
                .ForeignKey("dbo.PortfolioEntries", t => t.PortfolioEntry_PortfolioEntryID, cascadeDelete: true)
                .Index(t => t.Platform_PlatformID)
                .Index(t => t.PortfolioEntry_PortfolioEntryID);
            
            CreateTable(
                "dbo.ProgrammingLanguagePortfolioEntries",
                c => new
                    {
                        ProgrammingLanguage_ProgrammingLanguageID = c.Int(nullable: false),
                        PortfolioEntry_PortfolioEntryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProgrammingLanguage_ProgrammingLanguageID, t.PortfolioEntry_PortfolioEntryID })
                .ForeignKey("dbo.ProgrammingLanguages", t => t.ProgrammingLanguage_ProgrammingLanguageID, cascadeDelete: true)
                .ForeignKey("dbo.PortfolioEntries", t => t.PortfolioEntry_PortfolioEntryID, cascadeDelete: true)
                .Index(t => t.ProgrammingLanguage_ProgrammingLanguageID)
                .Index(t => t.PortfolioEntry_PortfolioEntryID);
            
            CreateTable(
                "dbo.TagPortfolioEntries",
                c => new
                    {
                        Tag_TagID = c.Int(nullable: false),
                        PortfolioEntry_PortfolioEntryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_TagID, t.PortfolioEntry_PortfolioEntryID })
                .ForeignKey("dbo.Tags", t => t.Tag_TagID, cascadeDelete: true)
                .ForeignKey("dbo.PortfolioEntries", t => t.PortfolioEntry_PortfolioEntryID, cascadeDelete: true)
                .Index(t => t.Tag_TagID)
                .Index(t => t.PortfolioEntry_PortfolioEntryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagPortfolioEntries", "PortfolioEntry_PortfolioEntryID", "dbo.PortfolioEntries");
            DropForeignKey("dbo.TagPortfolioEntries", "Tag_TagID", "dbo.Tags");
            DropForeignKey("dbo.ProgrammingLanguagePortfolioEntries", "PortfolioEntry_PortfolioEntryID", "dbo.PortfolioEntries");
            DropForeignKey("dbo.ProgrammingLanguagePortfolioEntries", "ProgrammingLanguage_ProgrammingLanguageID", "dbo.ProgrammingLanguages");
            DropForeignKey("dbo.PlatformPortfolioEntries", "PortfolioEntry_PortfolioEntryID", "dbo.PortfolioEntries");
            DropForeignKey("dbo.PlatformPortfolioEntries", "Platform_PlatformID", "dbo.Platforms");
            DropForeignKey("dbo.FrameworkPortfolioEntries", "PortfolioEntry_PortfolioEntryID", "dbo.PortfolioEntries");
            DropForeignKey("dbo.FrameworkPortfolioEntries", "Framework_FrameworkID", "dbo.Frameworks");
            DropForeignKey("dbo.PortfolioEntryCategories", "Category_CategoryID", "dbo.Categories");
            DropForeignKey("dbo.PortfolioEntryCategories", "PortfolioEntry_PortfolioEntryID", "dbo.PortfolioEntries");
            DropIndex("dbo.TagPortfolioEntries", new[] { "PortfolioEntry_PortfolioEntryID" });
            DropIndex("dbo.TagPortfolioEntries", new[] { "Tag_TagID" });
            DropIndex("dbo.ProgrammingLanguagePortfolioEntries", new[] { "PortfolioEntry_PortfolioEntryID" });
            DropIndex("dbo.ProgrammingLanguagePortfolioEntries", new[] { "ProgrammingLanguage_ProgrammingLanguageID" });
            DropIndex("dbo.PlatformPortfolioEntries", new[] { "PortfolioEntry_PortfolioEntryID" });
            DropIndex("dbo.PlatformPortfolioEntries", new[] { "Platform_PlatformID" });
            DropIndex("dbo.FrameworkPortfolioEntries", new[] { "PortfolioEntry_PortfolioEntryID" });
            DropIndex("dbo.FrameworkPortfolioEntries", new[] { "Framework_FrameworkID" });
            DropIndex("dbo.PortfolioEntryCategories", new[] { "Category_CategoryID" });
            DropIndex("dbo.PortfolioEntryCategories", new[] { "PortfolioEntry_PortfolioEntryID" });
            DropTable("dbo.TagPortfolioEntries");
            DropTable("dbo.ProgrammingLanguagePortfolioEntries");
            DropTable("dbo.PlatformPortfolioEntries");
            DropTable("dbo.FrameworkPortfolioEntries");
            DropTable("dbo.PortfolioEntryCategories");
            DropTable("dbo.Tags");
            DropTable("dbo.ProgrammingLanguages");
            DropTable("dbo.Platforms");
            DropTable("dbo.Frameworks");
            DropTable("dbo.PortfolioEntries");
            DropTable("dbo.Categories");
        }
    }
}
