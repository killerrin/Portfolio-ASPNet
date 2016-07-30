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
        public DataContext Context { get; }

        public PortfolioEntryRepository PortfolioEntryRepo { get; }
        public CategoryRepository CategoryRepo { get; }
        public FrameworkRepository FrameworkRepo { get; }
        public PlatformRepository PlatformRepo { get; }
        public ProgrammingLanguageRepository ProgrammingLanguageRepo { get; }
        public TagRepository TagRepo { get; }

        // Many-To-Many Collections
        public PortfolioEntryCategoryRepository PortfolioEntry_CategoryRepo { get; }
        public PortfolioEntryFrameworkRepository PortfolioEntry_FrameworkRepo { get; }
        public PortfolioEntryPlatformRepository PortfolioEntry_PlatformRepo { get; }
        public PortfolioEntryProgrammingLanguageRepository PortfolioEntry_ProgrammingLanguageRepo { get; }
        public PortfolioEntryTagRepository PortfolioEntry_TagRepo { get; }

        public PortfolioRepositoryCollection(DataContext context)
        {
            Context = context;

            PortfolioEntryRepo = new PortfolioEntryRepository(Context);
            CategoryRepo = new CategoryRepository(Context);
            FrameworkRepo = new FrameworkRepository(Context);
            PlatformRepo = new PlatformRepository(Context);
            ProgrammingLanguageRepo = new ProgrammingLanguageRepository(Context);
            TagRepo = new TagRepository(Context);

            // Many-to-Many collections
            PortfolioEntry_CategoryRepo = new PortfolioEntryCategoryRepository(Context);
            PortfolioEntry_FrameworkRepo = new PortfolioEntryFrameworkRepository(Context);
            PortfolioEntry_PlatformRepo = new PortfolioEntryPlatformRepository(Context);
            PortfolioEntry_ProgrammingLanguageRepo = new PortfolioEntryProgrammingLanguageRepository(Context);
            PortfolioEntry_TagRepo = new PortfolioEntryTagRepository(Context);
        }
    }
}
