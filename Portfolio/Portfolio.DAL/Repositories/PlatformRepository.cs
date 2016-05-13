using Portfolio.DAL.Data;
using Portfolio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.DAL.Repositories
{
    public class PlatformRepository: RepositoryBase<Platform>
    {
        public PlatformRepository(DataContext context)
            : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
    }
}
