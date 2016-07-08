using Portfolio.Model.Forms;
using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.WebUI.Areas.Admin.Models
{
    public class PortfolioEntryForm
    {
        public int PortfolioEntryId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Slug { get; set; }

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

        public List<CheckBoxItem> CheckedCategories { get; set; }
        public List<CheckBoxItem> CheckedProgrammingLanguages { get; set; }
        public List<CheckBoxItem> CheckedFrameworks { get; set; }
        public List<CheckBoxItem> CheckedPlatforms { get; set; }
        public List<CheckBoxItem> CheckedTags { get; set; }

        public PortfolioEntryForm()
        {
            CheckedCategories = new List<CheckBoxItem>();
            CheckedProgrammingLanguages = new List<CheckBoxItem>();
            CheckedFrameworks = new List<CheckBoxItem>();
            CheckedPlatforms = new List<CheckBoxItem>();
            CheckedTags = new List<CheckBoxItem>();
        }
    }
}
