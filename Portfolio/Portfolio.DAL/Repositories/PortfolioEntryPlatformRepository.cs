using Portfolio.DAL.Data;
using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.DAL.Repositories
{
    public class PortfolioEntryPlatformRepository : RepositoryBase<PortfolioEntryPlatform>
    {
        public PortfolioEntryPlatformRepository(DataContext context)
            : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
    }
}
