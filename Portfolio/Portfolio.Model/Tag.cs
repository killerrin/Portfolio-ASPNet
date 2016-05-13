using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Model
{
    public class Tag
    {
        public int TagID { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        public virtual ICollection<PortfolioEntry> PortfolioEntries { get; set; }
        public Tag()
        {
            PortfolioEntries = new List<PortfolioEntry>();
        }
    }
}
