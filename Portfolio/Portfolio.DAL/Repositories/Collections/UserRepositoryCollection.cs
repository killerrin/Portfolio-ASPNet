using Portfolio.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.DAL.Repositories.Collections
{
    public class UserRepositoryCollection
    {
        public DataContext Context { get; private set; }

        public UserRepository UserRepo { get; }
        public RoleRepository RoleRepo { get; }

        public UserRepositoryCollection(DataContext context)
        {
            Context = context;

            UserRepo = new UserRepository(Context);
            RoleRepo = new RoleRepository(Context);
        }
    }
}
