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

    public class TorrentsService : ITorrentsService
    {
        public void CreateTorrent(TorrentViewModel model)
        {
            // Set the provider name for the DynamicModuleManager here. All available providers are listed in
            // Administration -> Settings -> Advanced -> DynamicModules -> Providers
            var providerName = String.Empty;

            // Set a transaction name and get the version manager
            var transactionName = "someTransactionName";
            var versionManager = VersionManager.GetManager(null, transactionName);

            // Set the culture name for the multilingual fields
            var cultureName = "en";
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName);
            Type torrentType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Torrents.Torrent");
            DynamicContent torrentItem = dynamicModuleManager.CreateDataItem(torrentType);






            //// This is how values for the properties are set
            torrentItem.SetValue(nameof(model.Genre), model.Genre);
            //// Get related item manager
            //LibrariesManager imageManager = LibrariesManager.GetManager();
            //var imageItem = imageManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master);
            //if (imageItem != null)
            //{
            //    // This is how we relate an item
            //    torrentItem.CreateRelation(imageItem, nameof(model.Image));
            //}
           

            torrentItem.SetString(nameof(model.Title), model.Title, cultureName);
            torrentItem.SetString(nameof(model.AdditionalInfo), model.AdditionalInfo, cultureName);
            torrentItem.SetString(nameof(model.Description), model.Description, cultureName);
            torrentItem.SetString(nameof(model.DownloadLink), model.DownloadLink, cultureName);


            torrentItem.SetString("UrlName", "SomeUrlName", cultureName);
            torrentItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            torrentItem.SetValue("PublicationDate", DateTime.UtcNow);


            torrentItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo(cultureName));

            // Create a version and commit the transaction in order changes to be persisted to data store
            versionManager.CreateVersion(torrentItem, false); // true?
            TransactionManager.CommitTransaction(transactionName);

            // Use lifecycle so that LanguageData and other Multilingual related values are correctly created
            DynamicContent checkOutTorrentItem = dynamicModuleManager.Lifecycle.CheckOut(torrentItem) as DynamicContent;
            DynamicContent checkInTorrentItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutTorrentItem) as DynamicContent;
            versionManager.CreateVersion(checkInTorrentItem, false);
            TransactionManager.CommitTransaction(transactionName);
        }
    }
}