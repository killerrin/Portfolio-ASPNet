using Portfolio.DAL.Data;
using Portfolio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.DAL.Repositories
{
    public class ProgrammingLanguageRepository : RepositoryBase<ProgrammingLanguage>
    {
        public ProgrammingLanguageRepository(DataContext context)
            : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
    }
}
