using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Model
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        public virtual ICollection<PortfolioEntry> PortfolioEntries { get; set; }
        public Category()
        {
            PortfolioEntries = new List<PortfolioEntry>();
        }
    }
}
