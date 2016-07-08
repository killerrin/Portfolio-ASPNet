namespace Portfolio.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Portfolio_Items : DbMigration
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
                "dbo.Frameworks",
                c => new
                    {
                        FrameworkId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Slug = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.FrameworkId);
            
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
                "dbo.ProgrammingLanguages",
                c => new
                    {
                        ProgrammingLanguageId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Slug = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ProgrammingLanguageId);
            
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
                "dbo.PortfolioEntryCategories",
                c => new
                    {
                        PortfolioEntry_PortfolioEntryId = c.Int(nullable: false),
                        Category_CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PortfolioEntry_PortfolioEntryId, t.Category_CategoryId })
                .ForeignKey("dbo.PortfolioEntries", t => t.PortfolioEntry_PortfolioEntryId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId, cascadeDelete: true)
                .Index(t => t.PortfolioEntry_PortfolioEntryId)
                .Index(t => t.Category_CategoryId);
            
            CreateTable(
                "dbo.FrameworkPortfolioEntries",
                c => new
                    {
                        Framework_FrameworkId = c.Int(nullable: false),
                        PortfolioEntry_PortfolioEntryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Framework_FrameworkId, t.PortfolioEntry_PortfolioEntryId })
                .ForeignKey("dbo.Frameworks", t => t.Framework_FrameworkId, cascadeDelete: true)
                .ForeignKey("dbo.PortfolioEntries", t => t.PortfolioEntry_PortfolioEntryId, cascadeDelete: true)
                .Index(t => t.Framework_FrameworkId)
                .Index(t => t.PortfolioEntry_PortfolioEntryId);
            
            CreateTable(
                "dbo.PlatformPortfolioEntries",
                c => new
                    {
                        Platform_PlatformId = c.Int(nullable: false),
                        PortfolioEntry_PortfolioEntryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Platform_PlatformId, t.PortfolioEntry_PortfolioEntryId })
                .ForeignKey("dbo.Platforms", t => t.Platform_PlatformId, cascadeDelete: true)
                .ForeignKey("dbo.PortfolioEntries", t => t.PortfolioEntry_PortfolioEntryId, cascadeDelete: true)
                .Index(t => t.Platform_PlatformId)
                .Index(t => t.PortfolioEntry_PortfolioEntryId);
            
            CreateTable(
                "dbo.ProgrammingLanguagePortfolioEntries",
                c => new
                    {
                        ProgrammingLanguage_ProgrammingLanguageId = c.Int(nullable: false),
                        PortfolioEntry_PortfolioEntryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProgrammingLanguage_ProgrammingLanguageId, t.PortfolioEntry_PortfolioEntryId })
                .ForeignKey("dbo.ProgrammingLanguages", t => t.ProgrammingLanguage_ProgrammingLanguageId, cascadeDelete: true)
                .ForeignKey("dbo.PortfolioEntries", t => t.PortfolioEntry_PortfolioEntryId, cascadeDelete: true)
                .Index(t => t.ProgrammingLanguage_ProgrammingLanguageId)
                .Index(t => t.PortfolioEntry_PortfolioEntryId);
            
            CreateTable(
                "dbo.TagPortfolioEntries",
                c => new
                    {
                        Tag_TagId = c.Int(nullable: false),
                        PortfolioEntry_PortfolioEntryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_TagId, t.PortfolioEntry_PortfolioEntryId })
                .ForeignKey("dbo.Tags", t => t.Tag_TagId, cascadeDelete: true)
                .ForeignKey("dbo.PortfolioEntries", t => t.PortfolioEntry_PortfolioEntryId, cascadeDelete: true)
                .Index(t => t.Tag_TagId)
                .Index(t => t.PortfolioEntry_PortfolioEntryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagPortfolioEntries", "PortfolioEntry_PortfolioEntryId", "dbo.PortfolioEntries");
            DropForeignKey("dbo.TagPortfolioEntries", "Tag_TagId", "dbo.Tags");
            DropForeignKey("dbo.ProgrammingLanguagePortfolioEntries", "PortfolioEntry_PortfolioEntryId", "dbo.PortfolioEntries");
            DropForeignKey("dbo.ProgrammingLanguagePortfolioEntries", "ProgrammingLanguage_ProgrammingLanguageId", "dbo.ProgrammingLanguages");
            DropForeignKey("dbo.PlatformPortfolioEntries", "PortfolioEntry_PortfolioEntryId", "dbo.PortfolioEntries");
            DropForeignKey("dbo.PlatformPortfolioEntries", "Platform_PlatformId", "dbo.Platforms");
            DropForeignKey("dbo.FrameworkPortfolioEntries", "PortfolioEntry_PortfolioEntryId", "dbo.PortfolioEntries");
            DropForeignKey("dbo.FrameworkPortfolioEntries", "Framework_FrameworkId", "dbo.Frameworks");
            DropForeignKey("dbo.PortfolioEntryCategories", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.PortfolioEntryCategories", "PortfolioEntry_PortfolioEntryId", "dbo.PortfolioEntries");
            DropIndex("dbo.TagPortfolioEntries", new[] { "PortfolioEntry_PortfolioEntryId" });
            DropIndex("dbo.TagPortfolioEntries", new[] { "Tag_TagId" });
            DropIndex("dbo.ProgrammingLanguagePortfolioEntries", new[] { "PortfolioEntry_PortfolioEntryId" });
            DropIndex("dbo.ProgrammingLanguagePortfolioEntries", new[] { "ProgrammingLanguage_ProgrammingLanguageId" });
            DropIndex("dbo.PlatformPortfolioEntries", new[] { "PortfolioEntry_PortfolioEntryId" });
            DropIndex("dbo.PlatformPortfolioEntries", new[] { "Platform_PlatformId" });
            DropIndex("dbo.FrameworkPortfolioEntries", new[] { "PortfolioEntry_PortfolioEntryId" });
            DropIndex("dbo.FrameworkPortfolioEntries", new[] { "Framework_FrameworkId" });
            DropIndex("dbo.PortfolioEntryCategories", new[] { "Category_CategoryId" });
            DropIndex("dbo.PortfolioEntryCategories", new[] { "PortfolioEntry_PortfolioEntryId" });
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
