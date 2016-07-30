using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class PortfolioEntryProgrammingLanguage
    {
        [Key]
        public int PortfolioEntryProgrammingLanguageId { get; set; }

        public int PortfolioEntryId { get; set; }
        public int ProgrammingLanguageId { get; set; }

        public virtual PortfolioEntry PortfolioEntry { get; set; }
        public virtual ProgrammingLanguage ProgrammingLanguage { get; set; }
    }
}
