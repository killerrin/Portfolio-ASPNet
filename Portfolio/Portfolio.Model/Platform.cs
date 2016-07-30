using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class Platform
    {
        [Key]
        public int PlatformId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Slug { get; set; }

        public virtual ICollection<PortfolioEntryPlatform> PortfolioEntries { get; set; }
        public Platform()
        {
            PortfolioEntries = new List<PortfolioEntryPlatform>();
        }
    }
}
