using Portfolio.DAL.Data;
using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.DAL.Repositories
{
    public class PortfolioEntryProgrammingLanguageRepository : RepositoryBase<PortfolioEntryProgrammingLanguage>
    {
        public PortfolioEntryProgrammingLanguageRepository(DataContext context)
            : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
    }
}
