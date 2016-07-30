using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Slug { get; set; }

        public virtual ICollection<PortfolioEntryTag> PortfolioEntries { get; set; }
        public Tag()
        {
            PortfolioEntries = new List<PortfolioEntryTag>();
        }
    }
}
