using Portfolio.DAL.Repositories;
using Portfolio.Model.Forms;
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
    public class PortfolioEntryController : Controller
    {
        PortfolioEntryRepository PortfolioEntryRepo;
        CategoryRepository CategoryRepo;
        FrameworkRepository FrameworkRepo;
        PlatformRepository PlatformRepo;
        ProgrammingLanguageRepository ProgrammingLanguageRepo;
        TagRepository TagRepo;

        public PortfolioEntryController(PortfolioEntryRepository portfolioEntryRepo, CategoryRepository categoryRepo, FrameworkRepository frameworkRepo, PlatformRepository platformRepo, ProgrammingLanguageRepository programmingLanguageRepo, TagRepository tagRepo)
        {
            PortfolioEntryRepo = portfolioEntryRepo;

            CategoryRepo = categoryRepo;
            FrameworkRepo = frameworkRepo;
            PlatformRepo = platformRepo;
            ProgrammingLanguageRepo = programmingLanguageRepo;
            TagRepo = tagRepo;
        }

        // GET: Admin/PortfolioEntry
        public ActionResult Index()
        {
            var entries = PortfolioEntryRepo.GetAll().ToList();

            IndexPortfolioEntryViewModel viewModel = new IndexPortfolioEntryViewModel();
            viewModel.Published = entries.Where(x => x.UnpublishedAt == null).ToList();
            viewModel.Unpublished = entries.Where(x => x.UnpublishedAt != null).ToList();

            return View(viewModel);
        }

        public ActionResult Create()
        {
            PortfolioEntryForm form = new PortfolioEntryForm();
            form.CheckedCategories = CategoryRepo.GetAll().ToList().Select(x => new CheckBoxItem(x.CategoryId, x.Name)).ToList();
            form.CheckedProgrammingLanguages = ProgrammingLanguageRepo.GetAll().ToList().Select(x => new CheckBoxItem(x.ProgrammingLanguageId, x.Name)).ToList();
            form.CheckedFrameworks = FrameworkRepo.GetAll().ToList().Select(x => new CheckBoxItem(x.FrameworkId, x.Name)).ToList();
            form.CheckedPlatforms = PlatformRepo.GetAll().ToList().Select(x => new CheckBoxItem(x.PlatformId, x.Name)).ToList();
            form.CheckedTags = TagRepo.GetAll().ToList().Select(x => new CheckBoxItem(x.TagId, x.Name)).ToList();

            return View(form);
        }
        [HttpPost]
        public ActionResult Create(PortfolioEntryForm form)
        {
            var all = PortfolioEntryRepo.GetAll();
            if (all.Where(x => x.Name.Equals(form.Slug)).FirstOrDefault() != null)
                ModelState.AddModelError(nameof(form.Slug), "A Portfolio Entry with this Slug already exists");

            if (!ModelState.IsValid)
                return View(form);

            PortfolioEntry entry = new PortfolioEntry();
            entry.Name = form.Name;
            entry.Slug = form.Slug;
            entry.CreatedAt = DateTime.UtcNow;

            entry.CoverImageUrl = form.CoverImageUrl;
            entry.VideoEmbedUrl = form.VideoEmbedUrl;

            entry.SourceControlUrl = form.SourceControlUrl;
            entry.EmbedUrl = form.EmbedUrl;

            entry.Description = form.Description;
            entry.Features = form.Features;
            entry.Screenshots = form.Screenshots;

            entry.WebsiteUrl = form.WebsiteUrl;
            entry.GooglePlayStoreUrl = form.GooglePlayStoreUrl;
            entry.AppleAppStoreUrl = form.AppleAppStoreUrl;
            entry.MicrosoftWindowsStoreUrl = form.MicrosoftWindowsStoreUrl;
            entry.OtherMarketplaceUrls = form.OtherMarketplaceUrls;

            var categories = CategoryRepo.GetAll().ToList();
            categories.RemoveAll(x => form.CheckedCategories.Where(checkbox => checkbox.IsChecked).Where(checkbox => checkbox.ID == x.CategoryId).FirstOrDefault() != null);
            //entry.Categories = categories;

            var programminglanguages = ProgrammingLanguageRepo.GetAll().ToList();
            programminglanguages.RemoveAll(x => form.CheckedProgrammingLanguages.Where(checkbox => checkbox.IsChecked).Where(checkbox => checkbox.ID == x.ProgrammingLanguageId).FirstOrDefault() != null);
            //entry.ProgrammingLanguages = programminglanguages;

            var frameworks = FrameworkRepo.GetAll().ToList();
            frameworks.RemoveAll(x => form.CheckedFrameworks.Where(checkbox => checkbox.IsChecked).Where(checkbox => checkbox.ID == x.FrameworkId).FirstOrDefault() != null);
            //entry.Frameworks = frameworks;

            var platforms = PlatformRepo.GetAll().ToList();
            platforms.RemoveAll(x => form.CheckedPlatforms.Where(checkbox => checkbox.IsChecked).Where(checkbox => checkbox.ID == x.PlatformId).FirstOrDefault() != null);
            //entry.Platforms = platforms;

            var tags = TagRepo.GetAll().ToList();
            tags.RemoveAll(x => form.CheckedTags.Where(checkbox => checkbox.IsChecked).Where(checkbox => checkbox.ID == x.TagId).FirstOrDefault() != null);
            //entry.Tags = tags;

            PortfolioEntryRepo.Insert(entry);
            PortfolioEntryRepo.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var item = PortfolioEntryRepo.GetById(id);
            if (item == null)
                return RedirectToAction("Index");

            return View(item);
        }
        [HttpPost]
        public ActionResult Edit(PortfolioEntry entry, int id)
        {
            var all = PortfolioEntryRepo.GetAll().Where(x => x.PortfolioEntryId != entry.PortfolioEntryId);
            if (all.Where(x => x.Name.Equals(entry.Slug)).FirstOrDefault() != null)
                ModelState.AddModelError(nameof(entry.Slug), "A Portfolio Entry with this Slug already exists");

            if (!ModelState.IsValid)
                return View(entry);

            PortfolioEntryRepo.Update(entry);
            PortfolioEntryRepo.Commit();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Unpublish(int id)
        {
            var entry = PortfolioEntryRepo.GetById(id);
            entry.UnpublishedAt = DateTime.UtcNow;

            PortfolioEntryRepo.Update(entry);
            PortfolioEntryRepo.Commit();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Restore(int id)
        {
            var entry = PortfolioEntryRepo.GetById(id);
            entry.UnpublishedAt = null;

            PortfolioEntryRepo.Update(entry);
            PortfolioEntryRepo.Commit();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            PortfolioEntryRepo.Delete(id);
            PortfolioEntryRepo.Commit();
            return RedirectToAction("Index");
        }
    }
}