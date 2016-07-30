using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class PortfolioEntryFramework
    {
        [Key]
        public int PortfolioEntryFrameworkId { get; set; }

        public int PortfolioEntryId { get; set; }
        public int FrameworkId { get; set; }

        public virtual PortfolioEntry PortfolioEntry { get; set; }
        public virtual Framework Framework { get; set; }
    }
}
