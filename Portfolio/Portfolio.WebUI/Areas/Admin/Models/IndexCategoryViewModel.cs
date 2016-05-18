using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.WebUI.Areas.Admin.Models
{
    public class IndexCategoryViewModel
    {
        public List<Category> Categories = new List<Category>();
        public List<Framework> Frameworks = new List<Framework>();
        public List<Platform> Platforms = new List<Platform>();
        public List<ProgrammingLanguage> ProgrammingLanguages = new List<ProgrammingLanguage>();
        public List<Tag> Tags = new List<Tag>();

        public IndexCategoryViewModel()
        {

        }
    }
}
