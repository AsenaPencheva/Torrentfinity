namespace Torrentfinity.Sitefinity.Services.DynamicModules.Torrents
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using Telerik.Sitefinity;
    using Telerik.Sitefinity.Data;
    using Telerik.Sitefinity.DynamicModules;
    using Telerik.Sitefinity.DynamicModules.Model;
    using Telerik.Sitefinity.Model;
    using Telerik.Sitefinity.Modules.Libraries;
    using Telerik.Sitefinity.RelatedData;
    using Telerik.Sitefinity.Security;
    using Telerik.Sitefinity.Utilities.TypeConverters;
    using Telerik.Sitefinity.Versioning;
    using Torrentfinity.Mvc.Models;
    using Telerik.Sitefinity.Libraries.Model;
    using System.Web;
    using System.Text.RegularExpressions;
    using System.Collections.Generic;
    using Telerik.Sitefinity.Workflow;
    using Telerik.Sitefinity.Data.Linq.Dynamic;

    public class TorrentsService : ITorrentsService
    {
        public void CreateTorrent(TorrentViewModel model)
        {
            // validation for dublicate title
            var providerName = "OpenAccessProvider";
            var transactionName = "createTorrentTransaction";
            var versionManager = VersionManager.GetManager(null, transactionName);

            // Set the culture name for the multilingual fields
            var cultureName = "en";
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName);
            Type torrentType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Torrents.Torrent");

            var torrentItem = dynamicModuleManager.GetDataItems(torrentType).Where($"Title = \"{model.TitleEn}\"").FirstOrDefault();
            if (torrentItem != null)
            {
                throw new ArgumentException("Torrent with that title already exists!");
            }

            torrentItem = dynamicModuleManager.CreateDataItem(torrentType);

            torrentItem.SetValue(nameof(model.Genre), model.Genre);
            torrentItem.SetString(nameof(model.TitleEn), "Title", cultureName);
            torrentItem.SetString(nameof(model.AdditionalInfoEn),"AdditionalInfo", cultureName);
            torrentItem.SetString(nameof(model.DescriptionEn), "Description", cultureName);
            torrentItem.SetString(nameof(model.DownloadLink), "DownloadLink", cultureName);

            torrentItem.SetString("UrlName", model.TitleEn, cultureName);
            torrentItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            torrentItem.SetValue("PublicationDate", DateTime.UtcNow);

            Image imageItemId = this.CreateImage(model.Image);
            torrentItem.CreateRelation(imageItemId, nameof(model.Image));

            torrentItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo(cultureName));
            versionManager.CreateVersion(torrentItem, false);
            TransactionManager.CommitTransaction(transactionName);

            // Use lifecycle so that LanguageData and other Multilingual related values are correctly created
            DynamicContent checkOutTorrentItem = dynamicModuleManager.Lifecycle.CheckOut(torrentItem) as DynamicContent;
            DynamicContent checkInTorrentItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutTorrentItem) as DynamicContent;
            versionManager.CreateVersion(checkInTorrentItem, false);
            TransactionManager.CommitTransaction(transactionName);
        }

        private Image CreateImage(HttpPostedFileBase fileAttach)
        {
            LibrariesManager librariesManager = LibrariesManager.GetManager();
            Image image = librariesManager.CreateImage();
            Album album = librariesManager.GetAlbums().FirstOrDefault();
            image.Parent = album;
            image.Title = fileAttach.FileName;
            image.DateCreated = DateTime.UtcNow;
            image.PublicationDate = DateTime.UtcNow;
            image.LastModified = DateTime.UtcNow;
            image.UrlName = image.Id.ToString();
            image.MediaFileUrlName = Regex.Replace(fileAttach.FileName.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");

            string extension = "." + fileAttach.FileName.Split('.').Last();
            librariesManager.Upload(image, fileAttach.InputStream, extension);

            librariesManager.SaveChanges();

            var bag = new Dictionary<string, string>();
            bag.Add("ContentType", typeof(Image).FullName);
            WorkflowManager.MessageWorkflow(image.Id, typeof(Image), null, "Publish", false, bag);

            return image;
        }
    }
}