using Portfolio.DAL.Repositories;
using Portfolio.Models;
using Portfolio.WebUI.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portfolio.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        CategoryRepository CategoryRepo;
        FrameworkRepository FrameworkRepo;
        PlatformRepository PlatformRepo;
        ProgrammingLanguageRepository ProgrammingLanguageRepo;
        TagRepository TagRepo;

        public CategoryController(CategoryRepository categoryRepo, FrameworkRepository frameworkRepo, PlatformRepository platformRepo, ProgrammingLanguageRepository programmingLanguageRepo, TagRepository tagRepo)
        {
            CategoryRepo = categoryRepo;
            FrameworkRepo = frameworkRepo;
            PlatformRepo = platformRepo;
            ProgrammingLanguageRepo = programmingLanguageRepo;
            TagRepo = tagRepo;
        }

        // GET: Admin/Category
        public ActionResult Index()
        {
            IndexCategoryViewModel viewModel = new IndexCategoryViewModel();
            viewModel.Categories = CategoryRepo.GetAll().ToList(); ;
            viewModel.Frameworks = FrameworkRepo.GetAll().ToList(); ;
            viewModel.Platforms = PlatformRepo.GetAll().ToList(); ;
            viewModel.ProgrammingLanguages = ProgrammingLanguageRepo.GetAll().ToList(); ;
            viewModel.Tags = TagRepo.GetAll().ToList();

            return View(viewModel);
        }

        #region Category
        public ActionResult CreateCategory()
        {
            return View(new Category());
        }
        [HttpPost]
        public ActionResult CreateCategory(Category category)
        {
            var all = CategoryRepo.GetAll();
            if (all.Where(x => x.Name.Equals(category.Name)).FirstOrDefault() != null)
                ModelState.AddModelError(nameof(category.Name), "A Category with this Name already exists");
            if (all.Where(x => x.Name.Equals(category.Slug)).FirstOrDefault() != null)
                ModelState.AddModelError(nameof(category.Slug), "A Category with this Slug already exists");

            if (!ModelState.IsValid)
                return View(category);

            CategoryRepo.Insert(category);
            CategoryRepo.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult EditCategory(int id)
        {
            var item = CategoryRepo.GetById(id);
            if (item == null)
                return RedirectToAction("Index");

            return View(item);
        }
        [HttpPost]
        public ActionResult EditCategory(Category category, int id)
        {
            var all = CategoryRepo.GetAll().Where(x => x.CategoryId != category.CategoryId);
            if (all.Where(x => x.Name.Equals(category.Name)).FirstOrDefault() != null)
                ModelState.AddModelError(nameof(category.Name), "A Category with this Name already exists");
            if (all.Where(x => x.Name.Equals(category.Slug)).FirstOrDefault() != null)
                ModelState.AddModelError(nameof(category.Slug), "A Category with this Slug already exists");

            if (!ModelState.IsValid)
                return View(category);

            CategoryRepo.Update(category);
            CategoryRepo.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteCategory(int id)
        {
            CategoryRepo.Delete(id);
            CategoryRepo.Commit();
            return RedirectToAction("Index");
        }
        #endregion

        #region Framework
        public ActionResult CreateFramework()
        {
            return View(new Framework());
        }
        [HttpPost]
        public ActionResult CreateFramework(Framework framework)
        {
            var all = FrameworkRepo.GetAll();
            if (all.Where(x => x.Name.Equals(framework.Name)).FirstOrDefault() != null)
                ModelState.AddModelError(nameof(framework.Name), "A Framework with this Name already exists");
            if (all.Where(x => x.Name.Equals(framework.Slug)).FirstOrDefault() != null)
                ModelState.AddModelError(nameof(framework.Slug), "A Framework with this Slug already exists");

            if (!ModelState.IsValid)
                return View(framework);

            FrameworkRepo.Insert(framework);
            FrameworkRepo.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult EditFramework(int id)
        {
            var item = FrameworkRepo.GetById(id);
            if (item == null)
                return RedirectToAction("Index");

            return View(item);
        }
        [HttpPost]
        public ActionResult EditFramework(Framework framework, int id)
        {
            var all = FrameworkRepo.GetAll().Where(x => x.FrameworkId != framework.FrameworkId);
            if (all.Where(x => x.Name.Equals(framework.Name)).FirstOrDefault() != null)
                ModelState.AddModelError(nameof(framework.Name), "A Category with this Name already exists");
            if (all.Where(x => x.Name.Equals(framework.Slug)).FirstOrDefault() != null)
                ModelState.AddModelError(nameof(framework.Slug), "A Category with this Slug already exists");

            if (!ModelState.IsValid)
                return View(framework);

            FrameworkRepo.Update(framework);
            FrameworkRepo.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteFramework(int id)
        {
            FrameworkRepo.Delete(id);
            FrameworkRepo.Commit();
            return RedirectToAction("Index");
        }
        #endregion

        #region Platform
        public ActionResult CreatePlatform()
        {
            return View(new Platform());
        }
        [HttpPost]
        public ActionResult CreatePlatform(Platform platform)
        {
            var all = PlatformRepo.GetAll();
            if (all.Where(x => x.Name.Equals(platform.Name)).FirstOrDefault() != null)
                ModelState.AddModelError(nameof(platform.Name), "A Platform with this Name already exists");
            if (all.Where(x => x.Name.Equals(platform.Slug)).FirstOrDefault() != null)
                ModelState.AddModelError(nameof(platform.Slug), "A Platform with this Slug already exists");

            if (!ModelState.IsValid)
                return View(platform);

            PlatformRepo.Insert(platform);
            PlatformRepo.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult EditPlatform(int id)
        {
            var item = PlatformRepo.GetById(id);
            if (item == null)
                return RedirectToAction("Index");

            return View(item);
        }
        [HttpPost]
        public ActionResult EditPlatform(Platform platform, int id)
        {
            var all = PlatformRepo.GetAll().Where(x => x.PlatformId != platform.PlatformId);
            if (all.Where(x => x.Name.Equals(platform.Name)).FirstOrDefault() != null)
                ModelState.AddModelError(nameof(platform.Name), "A Platform with this Name already exists");
            if (all.Where(x => x.Name.Equals(platform.Slug)).FirstOrDefault() != null)
                ModelState.AddModelError(nameof(platform.Slug), "A Platform with this Slug already exists");

            if (!ModelState.IsValid)
                return View(platform);

            PlatformRepo.Update(platform);
            PlatformRepo.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult DeletePlatform(int id)
        {
            PlatformRepo.Delete(id);
            PlatformRepo.Commit();
            return RedirectToAction("Index");
        }
        #endregion

        #region Programming Language
        public ActionResult CreateProgrammingLanguage()
        {
            return View(new ProgrammingLanguage());
        }
        [HttpPost]
        public ActionResult CreateProgrammingLanguage(ProgrammingLanguage programmingLanguage)
        {
            var all = ProgrammingLanguageRepo.GetAll();
            if (all.Where(x => x.Name.Equals(programmingLanguage.Name)).FirstOrDefault() != null)
                ModelState.AddModelError(nameof(programmingLanguage.Name), "A Programming Language with this Name already exists");
            if (all.Where(x => x.Name.Equals(programmingLanguage.Slug)).FirstOrDefault() != null)
                ModelState.AddModelError(nameof(programmingLanguage.Slug), "A Programming Language with this Slug already exists");

            if (!ModelState.IsValid)
                return View(programmingLanguage);

            ProgrammingLanguageRepo.Insert(programmingLanguage);
            ProgrammingLanguageRepo.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult EditProgrammingLanguage(int id)
        {
            var item = ProgrammingLanguageRepo.GetById(id);
            if (item == null)
                return RedirectToAction("Index");

            return View(item);
        }
        [HttpPost]
        public ActionResult EditProgrammingLanguage(ProgrammingLanguage programmingLanguage, int id)
        {
            var all = ProgrammingLanguageRepo.GetAll().Where(x => x.ProgrammingLanguageId != programmingLanguage.ProgrammingLanguageId);
            if (all.Where(x => x.Name.Equals(programmingLanguage.Name)).FirstOrDefault() != null)
                ModelState.AddModelError(nameof(programmingLanguage.Name), "A Programming Language with this Name already exists");
            if (all.Where(x => x.Name.Equals(programmingLanguage.Slug)).FirstOrDefault() != null)
                ModelState.AddModelError(nameof(programmingLanguage.Slug), "A Programming Language with this Slug already exists");

            if (!ModelState.IsValid)
                return View(programmingLanguage);

            ProgrammingLanguageRepo.Update(programmingLanguage);
            ProgrammingLanguageRepo.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteProgrammingLanguage(int id)
        {
            ProgrammingLanguageRepo.Delete(id);
            ProgrammingLanguageRepo.Commit();
            return RedirectToAction("Index");
        }
        #endregion

        #region Tag
        public ActionResult CreateTag()
        {
            return View(new Tag());
        }
        [HttpPost]
        public ActionResult CreateTag(Tag tag)
        {
            var all = TagRepo.GetAll();
            if (all.Where(x => x.Name.Equals(tag.Name)).FirstOrDefault() != null)
                ModelState.AddModelError(nameof(tag.Name), "A Tag with this Name already exists");
            if (all.Where(x => x.Name.Equals(tag.Slug)).FirstOrDefault() != null)
                ModelState.AddModelError(nameof(tag.Slug), "A Tag with this Slug already exists");

            if (!ModelState.IsValid)
                return View(tag);

            TagRepo.Insert(tag);
            TagRepo.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult EditTag(int id)
        {
            var item = TagRepo.GetById(id);
            if (item == null)
                return RedirectToAction("Index");

            return View(item);
        }
        [HttpPost]
        public ActionResult EditTag(Tag tag, int id)
        {
            var all = TagRepo.GetAll().Where(x => x.TagId != tag.TagId);
            if (all.Where(x => x.Name.Equals(tag.Name)).FirstOrDefault() != null)
                ModelState.AddModelError(nameof(tag.Name), "A Tag with this Name already exists");
            if (all.Where(x => x.Name.Equals(tag.Slug)).FirstOrDefault() != null)
                ModelState.AddModelError(nameof(tag.Slug), "A Tag with this Slug already exists");

            if (!ModelState.IsValid)
                return View(tag);

            TagRepo.Update(tag);
            TagRepo.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteTag(int id)
        {
            TagRepo.Delete(id);
            TagRepo.Commit();
            return RedirectToAction("Index");
        }
        #endregion
    }
}