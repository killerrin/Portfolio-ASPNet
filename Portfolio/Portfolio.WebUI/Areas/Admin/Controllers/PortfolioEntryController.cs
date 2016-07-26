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

            var entry = ConvertToEntry(form);
            entry.CreatedAt = DateTime.UtcNow;

            var reconciledTags = ReconcileTags(form);
            entry.Categories = reconciledTags.Categories;
            entry.ProgrammingLanguages = reconciledTags.ProgrammingLanguages;
            entry.Frameworks = reconciledTags.Frameworks;
            entry.Platforms = reconciledTags.Platforms;
            entry.Tags = reconciledTags.Tags;

            PortfolioEntryRepo.Insert(entry);
            PortfolioEntryRepo.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var item = PortfolioEntryRepo.GetById(id);
            if (item == null)
                return RedirectToAction("Index");

            PortfolioEntryForm form = new PortfolioEntryForm();
            form.PortfolioEntryId = item.PortfolioEntryId;
            form.Name = item.Name;
            form.Slug = item.Slug;

            form.CoverImageUrl = item.CoverImageUrl;
            form.VideoEmbedUrl = item.VideoEmbedUrl;

            form.SourceControlUrl = item.SourceControlUrl;
            form.EmbedUrl = item.EmbedUrl;

            form.Description = item.Description;
            form.Features = item.Features;
            form.Screenshots = item.Screenshots;

            form.WebsiteUrl = item.WebsiteUrl;
            form.GooglePlayStoreUrl = item.GooglePlayStoreUrl;
            form.AppleAppStoreUrl = item.AppleAppStoreUrl;
            form.MicrosoftWindowsStoreUrl = item.MicrosoftWindowsStoreUrl;
            form.OtherMarketplaceUrls = item.OtherMarketplaceUrls;

            form.CheckedCategories = CategoryRepo.GetAll().ToList().Select(x => new CheckBoxItem(x.CategoryId, x.Name)).ToList();
            foreach (var checkBox in form.CheckedCategories)
                foreach (var itemCategory in item.Categories)
                    if (checkBox.ID == itemCategory.CategoryId)
                        checkBox.IsChecked = true;

            form.CheckedProgrammingLanguages = ProgrammingLanguageRepo.GetAll().ToList().Select(x => new CheckBoxItem(x.ProgrammingLanguageId, x.Name)).ToList();
            foreach (var checkBox in form.CheckedProgrammingLanguages)
                foreach (var itemCategory in item.ProgrammingLanguages)
                    if (checkBox.ID == itemCategory.ProgrammingLanguageId)
                        checkBox.IsChecked = true;

            form.CheckedFrameworks = FrameworkRepo.GetAll().ToList().Select(x => new CheckBoxItem(x.FrameworkId, x.Name)).ToList();
            foreach (var checkBox in form.CheckedFrameworks)
                foreach (var itemCategory in item.Frameworks)
                    if (checkBox.ID == itemCategory.FrameworkId)
                        checkBox.IsChecked = true;

            form.CheckedPlatforms = PlatformRepo.GetAll().ToList().Select(x => new CheckBoxItem(x.PlatformId, x.Name)).ToList();
            foreach (var checkBox in form.CheckedPlatforms)
                foreach (var itemCategory in item.Platforms)
                    if (checkBox.ID == itemCategory.PlatformId)
                        checkBox.IsChecked = true;

            form.CheckedTags = TagRepo.GetAll().ToList().Select(x => new CheckBoxItem(x.TagId, x.Name)).ToList();
            foreach (var checkBox in form.CheckedTags)
                foreach (var itemCategory in item.Tags)
                    if (checkBox.ID == itemCategory.TagId)
                        checkBox.IsChecked = true;

            return View(form);
        }
        [HttpPost]
        public ActionResult Edit(PortfolioEntryForm form, int id)
        {
            var all = PortfolioEntryRepo.GetAll().Where(x => x.PortfolioEntryId != form.PortfolioEntryId);
            if (all.Where(x => x.Name.Equals(form.Slug)).FirstOrDefault() != null)
                ModelState.AddModelError(nameof(form.Slug), "A Portfolio Entry with this Slug already exists");

            if (!ModelState.IsValid)
                return View(form);

            var entry = ConvertToEntry(form);
            var reconciledTags = ReconcileTags(form);

            PortfolioEntryRepo.Context.Entry(entry).Collection("Categories").Load();
            entry.Categories.Clear();
            foreach (var item in reconciledTags.Categories)
            {
                entry.Categories.Add(item);
            }

            PortfolioEntryRepo.Context.Entry(entry).Collection("ProgrammingLanguages").Load();
            entry.Categories.Clear();
            foreach (var item in reconciledTags.ProgrammingLanguages)
            {
                entry.ProgrammingLanguages.Add(item);
            }

            PortfolioEntryRepo.Context.Entry(entry).Collection("Frameworks").Load();
            entry.Categories.Clear();
            foreach (var item in reconciledTags.Frameworks)
            {
                entry.Frameworks.Add(item);
            }

            PortfolioEntryRepo.Context.Entry(entry).Collection("Platforms").Load();
            entry.Categories.Clear();
            foreach (var item in reconciledTags.Platforms)
            {
                entry.Platforms.Add(item);
            }

            PortfolioEntryRepo.Context.Entry(entry).Collection("Tags").Load();
            entry.Categories.Clear();
            foreach (var item in reconciledTags.Tags)
            {
                entry.Tags.Add(item);
            }

            PortfolioEntryRepo.Update(entry);
            PortfolioEntryRepo.Commit();
            return RedirectToAction("Index");
        }

        private PortfolioEntry ConvertToEntry(PortfolioEntryForm form)
        {
            PortfolioEntry entry = new PortfolioEntry();
            if (form.PortfolioEntryId != 0)
                entry = PortfolioEntryRepo.GetById(form.PortfolioEntryId);

            entry.Name = form.Name;
            entry.Slug = form.Slug;

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

            return entry;
        }

        private PortfolioEntry ReconcileTags(PortfolioEntryForm form)
        {
            PortfolioEntry entry = new PortfolioEntry();
            var categories = CategoryRepo.GetAll().ToList();
            categories.RemoveAll(x => form.CheckedCategories.Where(checkbox => !checkbox.IsChecked).Where(checkbox => checkbox.ID == x.CategoryId).FirstOrDefault() != null);
            entry.Categories = categories;

            var programminglanguages = ProgrammingLanguageRepo.GetAll().ToList();
            programminglanguages.RemoveAll(x => form.CheckedProgrammingLanguages.Where(checkbox => !checkbox.IsChecked).Where(checkbox => checkbox.ID == x.ProgrammingLanguageId).FirstOrDefault() != null);
            entry.ProgrammingLanguages = programminglanguages;

            var frameworks = FrameworkRepo.GetAll().ToList();
            frameworks.RemoveAll(x => form.CheckedFrameworks.Where(checkbox => !checkbox.IsChecked).Where(checkbox => checkbox.ID == x.FrameworkId).FirstOrDefault() != null);
            entry.Frameworks = frameworks;

            var platforms = PlatformRepo.GetAll().ToList();
            platforms.RemoveAll(x => form.CheckedPlatforms.Where(checkbox => !checkbox.IsChecked).Where(checkbox => checkbox.ID == x.PlatformId).FirstOrDefault() != null);
            entry.Platforms = platforms;

            var tags = TagRepo.GetAll().ToList();
            tags.RemoveAll(x => form.CheckedTags.Where(checkbox => !checkbox.IsChecked).Where(checkbox => checkbox.ID == x.TagId).FirstOrDefault() != null);
            entry.Tags = tags;

            return entry;
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