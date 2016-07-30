using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class PortfolioEntry
    {
        [Key]
        public int PortfolioEntryId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UnpublishedAt { get; set; }

        public string CoverImageUrl { get; set; }
        public string VideoEmbedUrl { get; set; }

        public string SourceControlUrl { get; set; }
        public string EmbedUrl { get; set; }

        public string Description { get; set; }
        public string Features { get; set; }
        public string Screenshots { get; set; }

        public string WebsiteUrl { get; set; }
        public string GooglePlayStoreUrl { get; set; }
        public string AppleAppStoreUrl { get; set; }
        public string MicrosoftWindowsStoreUrl { get; set; }
        public string OtherMarketplaceUrls { get; set; }

        public virtual ICollection<PortfolioEntryCategory> Categories { get; set; }
        public virtual ICollection<PortfolioEntryFramework> Frameworks { get; set; }
        public virtual ICollection<PortfolioEntryPlatform> Platforms { get; set; }
        public virtual ICollection<PortfolioEntryProgrammingLanguage> ProgrammingLanguages { get; set; }
        public virtual ICollection<PortfolioEntryTag> Tags { get; set; }
        public PortfolioEntry()
        {
            Categories = new List<PortfolioEntryCategory>();
            Frameworks = new List<PortfolioEntryFramework>();
            Platforms = new List<PortfolioEntryPlatform>();
            ProgrammingLanguages = new List<PortfolioEntryProgrammingLanguage>();
            Tags = new List<PortfolioEntryTag>();
        }
    }
}
