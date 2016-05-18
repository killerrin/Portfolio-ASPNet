using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class ProgrammingLanguage
    {
        [Key]
        public int ProgrammingLanguageId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Slug { get; set; }

        public virtual ICollection<PortfolioEntry> PortfolioEntries { get; set; }
        public ProgrammingLanguage()
        {
            PortfolioEntries = new List<PortfolioEntry>();
        }
    }
}
