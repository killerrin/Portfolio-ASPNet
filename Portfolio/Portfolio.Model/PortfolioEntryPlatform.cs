using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class PortfolioEntryPlatform
    {
        [Key]
        public int PortfolioEntryPlatformId { get; set; }

        public int PortfolioEntryId { get; set; }
        public int PlatformId { get; set; }

        public virtual PortfolioEntry PortfolioEntry { get; set; }
        public virtual Platform Platform { get; set; }
    }
}
