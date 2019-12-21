namespace Torrentfinity.Sitefinity.Services.DynamicModules.Torrents
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using Telerik.Microsoft.Practices.Unity.Utility;
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

    public class TorrentsService : ITorrentsService
    {
        private readonly IGenresService genresService;

        public TorrentsService(IGenresService genresService)
        {
            Guard.ArgumentNotNull(genresService, nameof(genresService));

            this.genresService = genresService;
        }

        public void CreateTorrent(TorrentViewModel model)
        {
            // Set the provider name for the DynamicModuleManager here. All available providers are listed in
            // Administration -> Settings -> Advanced -> DynamicModules -> Providers
            var providerName = "OpenAccessProvider";

            // Set a transaction name and get the version manager
            var transactionName = "createTorrentTransaction";
            var versionManager = VersionManager.GetManager(null, transactionName);

            // Set the culture name for the multilingual fields
            var cultureName = "en";
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName);
            Type torrentType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Torrents.Torrent");
            DynamicContent torrentItem = dynamicModuleManager.CreateDataItem(torrentType);

            DynamicContent genreType = this.genresService.Get(model.Genre);
            if (genreType == null)
            {
                genreType = this.genresService.CreateGenre(model.Genre);
            }
            torrentItem.CreateRelation(genreType, nameof(model.Genre));

            torrentItem.SetString(nameof(model.Title), model.Title, cultureName);
            torrentItem.SetString(nameof(model.AdditionalInfo), model.AdditionalInfo, cultureName);
            torrentItem.SetString(nameof(model.Description), model.Description, cultureName);
            torrentItem.SetString(nameof(model.DownloadLink), model.DownloadLink, cultureName);

            torrentItem.SetString("UrlName", "SomeUrlName", cultureName);
            torrentItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            torrentItem.SetValue("PublicationDate", DateTime.UtcNow);


            // Get related item manager
            LibrariesManager imageManager = LibrariesManager.GetManager();
            Image imageItem = this.CreateImage(model.FileAttach);
            //   imageManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master);
            if (imageItem != null)
            {
                torrentItem.CreateRelation(imageItem, "Image");
            }

            torrentItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo(cultureName)); // draft???

            // Create a version and commit the transaction in order changes to be persisted to data store
            versionManager.CreateVersion(torrentItem, false); // true?
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
            //The album post is created as master. The masterImageId is assigned to the master version.
            Image image = librariesManager.CreateImage();

            //Set the parent album.
            Album album = librariesManager.GetAlbums().FirstOrDefault(); // get the default album
            image.Parent = album;

            //Set the properties of the album post.
            image.Title = fileAttach.FileName;
            image.DateCreated = DateTime.UtcNow;
            image.PublicationDate = DateTime.UtcNow;
            image.LastModified = DateTime.UtcNow;
            image.UrlName = Regex.Replace(fileAttach.FileName.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");
            image.MediaFileUrlName = Regex.Replace(fileAttach.FileName.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");

            //Upload the image file.
            // The imageExtension parameter must contain '.', for example '.jpeg'
            librariesManager.Upload(image, fileAttach.InputStream, "." + fileAttach.FileName.Split('.').Last());  // need refactor

            //Save the changes.
            librariesManager.SaveChanges();

            //Publish the Albums item. The live version acquires new ID.
            var bag = new Dictionary<string, string>();
            bag.Add("ContentType", typeof(Image).FullName);
            WorkflowManager.MessageWorkflow(image.Id, typeof(Image), null, "Publish", false, bag);

            return image;
        }
    }
}