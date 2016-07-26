using Portfolio.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.DAL.Repositories.Collections
{
    public class PortfolioRepositoryCollection
    {
        public DataContext Context { get; private set; }

        public PortfolioEntryRepository PortfolioEntryRepo;
        public CategoryRepository CategoryRepo;
        public FrameworkRepository FrameworkRepo;
        public PlatformRepository PlatformRepo;
        public ProgrammingLanguageRepository ProgrammingLanguageRepo;
        public TagRepository TagRepo;

        public PortfolioRepositoryCollection(DataContext context)
        {
            Context = context;

            PortfolioEntryRepo = new PortfolioEntryRepository(Context);
            CategoryRepo = new CategoryRepository(Context);
            FrameworkRepo = new FrameworkRepository(Context);
            PlatformRepo = new PlatformRepository(Context);
            ProgrammingLanguageRepo = new ProgrammingLanguageRepository(Context);
            TagRepo = new TagRepository(Context);
        }
    }
}
