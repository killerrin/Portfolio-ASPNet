using Microsoft.Practices.ObjectBuilder2;
using Portfolio.DAL.Repositories;
using Portfolio.DAL.Repositories.Collections;
using Portfolio.Model.Forms;
using Portfolio.Models;
using Portfolio.WebUI.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portfolio.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PortfolioEntryController : Controller
    {
        PortfolioRepositoryCollection Repositories;
        public PortfolioEntryController(PortfolioRepositoryCollection repoCollection)
        {
            Repositories = repoCollection;
        }

        // GET: Admin/PortfolioEntry
        public ActionResult Index()
        {
            var entries = Repositories.PortfolioEntryRepo.GetAll().ToList();

            IndexPortfolioEntryViewModel viewModel = new IndexPortfolioEntryViewModel();
            viewModel.Published = entries.Where(x => x.UnpublishedAt == null).ToList();
            viewModel.Unpublished = entries.Where(x => x.UnpublishedAt != null).ToList();

            return View(viewModel);
        }

        public ActionResult Create()
        {
            PortfolioEntryForm form = new PortfolioEntryForm();
            form.CheckedCategories = Repositories.CategoryRepo.GetAll().ToList().Select(x => new CheckBoxItem(x.CategoryId, x.Name)).ToList();
            form.CheckedProgrammingLanguages = Repositories.ProgrammingLanguageRepo.GetAll().ToList().Select(x => new CheckBoxItem(x.ProgrammingLanguageId, x.Name)).ToList();
            form.CheckedFrameworks = Repositories.FrameworkRepo.GetAll().ToList().Select(x => new CheckBoxItem(x.FrameworkId, x.Name)).ToList();
            form.CheckedPlatforms = Repositories.PlatformRepo.GetAll().ToList().Select(x => new CheckBoxItem(x.PlatformId, x.Name)).ToList();
            form.CheckedTags = Repositories.TagRepo.GetAll().ToList().Select(x => new CheckBoxItem(x.TagId, x.Name)).ToList();

            return View(form);
        }
        [HttpPost]
        public ActionResult Create(PortfolioEntryForm form)
        {
            var all = Repositories.PortfolioEntryRepo.GetAll();
            if (all.Where(x => x.Name.Equals(form.Slug)).FirstOrDefault() != null)
                ModelState.AddModelError(nameof(form.Slug), "A Portfolio Entry with this Slug already exists");

            if (!ModelState.IsValid)
                return View(form);

            var entry = ConvertToEntry(form);
            entry.CreatedAt = DateTime.UtcNow;

            Repositories.PortfolioEntryRepo.Insert(entry);
            Repositories.PortfolioEntryRepo.Commit();

            Debug.WriteLine($"PortfolioEntry created with ID: {entry.PortfolioEntryId}");

            var reconciledTags = ReconcileTags(form);
            foreach (var tag in reconciledTags.Item1)
            {
                var x = new PortfolioEntryCategory();
                x.CategoryId = tag.CategoryId;
                x.PortfolioEntryId = entry.PortfolioEntryId;
                Repositories.PortfolioEntry_CategoryRepo.Insert(x);
            }
            Repositories.PortfolioEntry_CategoryRepo.Commit();

            foreach (var tag in reconciledTags.Item2)
            {
                var x = new PortfolioEntryFramework();
                x.FrameworkId = tag.FrameworkId;
                x.PortfolioEntryId = entry.PortfolioEntryId;
                Repositories.PortfolioEntry_FrameworkRepo.Insert(x);
            }
            Repositories.PortfolioEntry_FrameworkRepo.Commit();

            foreach (var tag in reconciledTags.Item3)
            {
                var x = new PortfolioEntryPlatform();
                x.PlatformId = tag.PlatformId;
                x.PortfolioEntryId = entry.PortfolioEntryId;
                Repositories.PortfolioEntry_PlatformRepo.Insert(x);
            }
            Repositories.PortfolioEntry_PlatformRepo.Commit();

            foreach (var tag in reconciledTags.Item4)
            {
                var x = new PortfolioEntryProgrammingLanguage();
                x.ProgrammingLanguageId = tag.ProgrammingLanguageId;
                x.PortfolioEntryId = entry.PortfolioEntryId;
                Repositories.PortfolioEntry_ProgrammingLanguageRepo.Insert(x);
            }
            Repositories.PortfolioEntry_ProgrammingLanguageRepo.Commit();

            foreach (var tag in reconciledTags.Item5)
            {
                var x = new PortfolioEntryTag();
                x.TagId = tag.TagId;
                x.PortfolioEntryId = entry.PortfolioEntryId;
                Repositories.PortfolioEntry_TagRepo.Insert(x);
            }
            Repositories.PortfolioEntry_TagRepo.Commit();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var item = Repositories.PortfolioEntryRepo.GetById(id);
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

            form.CheckedCategories = Repositories.CategoryRepo.GetAll().ToList().Select(x => new CheckBoxItem(x.CategoryId, x.Name)).ToList();
            foreach (var checkBox in form.CheckedCategories)
                foreach (var itemCategory in item.Categories)
                    if (checkBox.ID == itemCategory.CategoryId)
                        checkBox.IsChecked = true;

            form.CheckedProgrammingLanguages = Repositories.ProgrammingLanguageRepo.GetAll().ToList().Select(x => new CheckBoxItem(x.ProgrammingLanguageId, x.Name)).ToList();
            foreach (var checkBox in form.CheckedProgrammingLanguages)
                foreach (var itemCategory in item.ProgrammingLanguages)
                    if (checkBox.ID == itemCategory.ProgrammingLanguageId)
                        checkBox.IsChecked = true;

            form.CheckedFrameworks = Repositories.FrameworkRepo.GetAll().ToList().Select(x => new CheckBoxItem(x.FrameworkId, x.Name)).ToList();
            foreach (var checkBox in form.CheckedFrameworks)
                foreach (var itemCategory in item.Frameworks)
                    if (checkBox.ID == itemCategory.FrameworkId)
                        checkBox.IsChecked = true;

            form.CheckedPlatforms = Repositories.PlatformRepo.GetAll().ToList().Select(x => new CheckBoxItem(x.PlatformId, x.Name)).ToList();
            foreach (var checkBox in form.CheckedPlatforms)
                foreach (var itemCategory in item.Platforms)
                    if (checkBox.ID == itemCategory.PlatformId)
                        checkBox.IsChecked = true;

            form.CheckedTags = Repositories.TagRepo.GetAll().ToList().Select(x => new CheckBoxItem(x.TagId, x.Name)).ToList();
            foreach (var checkBox in form.CheckedTags)
                foreach (var itemCategory in item.Tags)
                    if (checkBox.ID == itemCategory.TagId)
                        checkBox.IsChecked = true;

            return View(form);
        }
        [HttpPost]
        public ActionResult Edit(PortfolioEntryForm form, int id)
        {
            var all = Repositories.PortfolioEntryRepo.GetAll().Where(x => x.PortfolioEntryId != form.PortfolioEntryId);
            if (all.Where(x => x.Name.Equals(form.Slug)).FirstOrDefault() != null)
                ModelState.AddModelError(nameof(form.Slug), "A Portfolio Entry with this Slug already exists");

            if (!ModelState.IsValid)
                return View(form);

            // Update the Entity's Fields
            var entry = ConvertToEntry(form);

            Repositories.PortfolioEntryRepo.Update(entry);
            Repositories.PortfolioEntryRepo.Commit();
            return RedirectToAction("Index");
        }

        private PortfolioEntry ConvertToEntry(PortfolioEntryForm form)
        {
            PortfolioEntry entry = new PortfolioEntry();
            if (form.PortfolioEntryId != 0)
                entry = Repositories.PortfolioEntryRepo.GetById(form.PortfolioEntryId);

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

        private Tuple<List<Category>, List<Framework>, List<Platform>, List<ProgrammingLanguage>, List<Tag>> ReconcileTags(PortfolioEntryForm form)
        {
            var categories = Repositories.CategoryRepo.GetAll().ToList();
            categories.RemoveAll(x => form.CheckedCategories.Where(checkbox => !checkbox.IsChecked).Where(checkbox => checkbox.ID == x.CategoryId).FirstOrDefault() != null);

            var frameworks = Repositories.FrameworkRepo.GetAll().ToList();
            frameworks.RemoveAll(x => form.CheckedFrameworks.Where(checkbox => !checkbox.IsChecked).Where(checkbox => checkbox.ID == x.FrameworkId).FirstOrDefault() != null);

            var platforms = Repositories.PlatformRepo.GetAll().ToList();
            platforms.RemoveAll(x => form.CheckedPlatforms.Where(checkbox => !checkbox.IsChecked).Where(checkbox => checkbox.ID == x.PlatformId).FirstOrDefault() != null);

            var programminglanguages = Repositories.ProgrammingLanguageRepo.GetAll().ToList();
            programminglanguages.RemoveAll(x => form.CheckedProgrammingLanguages.Where(checkbox => !checkbox.IsChecked).Where(checkbox => checkbox.ID == x.ProgrammingLanguageId).FirstOrDefault() != null);

            var tags = Repositories.TagRepo.GetAll().ToList();
            tags.RemoveAll(x => form.CheckedTags.Where(checkbox => !checkbox.IsChecked).Where(checkbox => checkbox.ID == x.TagId).FirstOrDefault() != null);

            return new Tuple<List<Category>, List<Framework>, List<Platform>, List<ProgrammingLanguage>, List<Tag>>(
                categories,
                frameworks,
                platforms,
                programminglanguages,
                tags);
        }

        [HttpPost]
        public ActionResult Unpublish(int id)
        {
            var entry = Repositories.PortfolioEntryRepo.GetById(id);
            entry.UnpublishedAt = DateTime.UtcNow;

            Repositories.PortfolioEntryRepo.Update(entry);
            Repositories.PortfolioEntryRepo.Commit();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Restore(int id)
        {
            var entry = Repositories.PortfolioEntryRepo.GetById(id);
            entry.UnpublishedAt = null;

            Repositories.PortfolioEntryRepo.Update(entry);
            Repositories.PortfolioEntryRepo.Commit();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Repositories.PortfolioEntryRepo.Delete(id);
            Repositories.PortfolioEntryRepo.Commit();
            return RedirectToAction("Index");
        }
    }
}