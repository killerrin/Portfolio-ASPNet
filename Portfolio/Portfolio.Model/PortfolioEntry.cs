using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Model
{
    public class PortfolioEntry
    {
        public int PortfolioEntryID { get; set; }
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

        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public virtual ICollection<Framework> Frameworks { get; set; }
        public virtual ICollection<Platform> Platforms { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public PortfolioEntry()
        {
            Categories = new List<Category>();
            ProgrammingLanguages = new List<ProgrammingLanguage>();
            Frameworks = new List<Framework>();
            Platforms = new List<Platform>();
            Tags = new List<Tag>();
        }
    }
}
