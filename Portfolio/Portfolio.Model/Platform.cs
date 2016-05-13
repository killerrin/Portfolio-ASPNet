using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Model
{
    public class Platform
    {
        public int PlatformID { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        public virtual ICollection<PortfolioEntry> PortfolioEntries { get; set; }
        public Platform()
        {
            PortfolioEntries = new List<PortfolioEntry>();
        }
    }
}
