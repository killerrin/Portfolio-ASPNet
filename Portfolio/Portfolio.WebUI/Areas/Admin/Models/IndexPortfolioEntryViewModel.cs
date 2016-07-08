using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.WebUI.Areas.Admin.Models
{
    public class IndexPortfolioEntryViewModel
    {
        public List<PortfolioEntry> Published = new List<PortfolioEntry>();
        public List<PortfolioEntry> Unpublished = new List<PortfolioEntry>();

        public IndexPortfolioEntryViewModel()
        {

        }
    }
}
