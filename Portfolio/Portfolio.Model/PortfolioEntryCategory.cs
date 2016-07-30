using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class PortfolioEntryCategory
    {
        [Key]
        public int PortfolioEntryCategoryId { get; set; }

        public int PortfolioEntryId { get; set; }
        public int CategoryId { get; set; }

        public virtual PortfolioEntry PortfolioEntry { get; set; }
        public virtual Category Category { get; set; }
    }
}
