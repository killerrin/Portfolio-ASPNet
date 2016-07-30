using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class PortfolioEntryTag
    {
        [Key]
        public int PortfolioEntryTagId { get; set; }

        public int PortfolioEntryId { get; set; }
        public int TagId { get; set; }

        public virtual PortfolioEntry PortfolioEntry { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
