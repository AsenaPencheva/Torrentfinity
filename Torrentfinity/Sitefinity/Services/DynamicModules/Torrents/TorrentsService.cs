namespace Torrentfinity.Sitefinity.Services.DynamicModules.Torrents
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using Telerik.Sitefinity;
    using Telerik.Sitefinity.DynamicModules;
    using Telerik.Sitefinity.DynamicModules.Model;
    using Telerik.Sitefinity.Model;
    using Telerik.Sitefinity.RelatedData;
    using Telerik.Sitefinity.Versioning;
    using Torrentfinity.Mvc.Models;
    using Telerik.Sitefinity.Libraries.Model;
    using System.Collections.Generic;
    using Telerik.Sitefinity.Data.Linq.Dynamic;
    using Torrentfinity.Sitefinity.Services.DynamicModules.BuldInContents;
    using Telerik.Microsoft.Practices.Unity.Utility;
    using Torrentfinity.Sitefinity.Common.Providers;

    public class TorrentsService : ITorrentsService
    {
        private readonly IImagesService imagesService;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IManagerProvider managerProvider;
        private readonly IEnumerable<string> avaliableLanguages = new List<string> { "en", "bg" };

        public TorrentsService(IImagesService imagesService, IDateTimeProvider dateTimeProvider, IManagerProvider managerProvider)
        {
            Guard.ArgumentNotNull(imagesService, nameof(imagesService));
            Guard.ArgumentNotNull(dateTimeProvider, nameof(dateTimeProvider));
            Guard.ArgumentNotNull(managerProvider, nameof(managerProvider));

            this.imagesService = imagesService;
            this.dateTimeProvider = dateTimeProvider;
            this.managerProvider = managerProvider;
        }

        public void CreateTorrent(TorrentViewModel model)
        {
            Guard.ArgumentNotNull(model, nameof(model));

            string providerName = "OpenAccessProvider";
            string transactionName = "createTorrentTransaction";
            VersionManager versionManager = managerProvider.GetVersionManager(null, transactionName);

            string cultureName = "en";
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);

            DynamicModuleManager dynamicModuleManager = managerProvider.GetDynamicModuleManager(providerName, transactionName);
            Type torrentType = this.managerProvider.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Torrents.Torrent");

            string titleEn = model.LanguageContents.FirstOrDefault(x => x.Language == "en")?.Title;
            
            DynamicContent torrentItem = dynamicModuleManager.GetDataItems(torrentType).Where($"Title = \"{titleEn}\"").FirstOrDefault();
            if (torrentItem != null)
            {
                throw new ArgumentException("Torrent with that title already exists!");
            }

            torrentItem = dynamicModuleManager.CreateDataItem(torrentType);
            foreach (var languageContent in model.LanguageContents)
            {
                torrentItem.SetString("Title", languageContent.Title, languageContent.Language);
                torrentItem.SetString("Description", languageContent.Description, languageContent.Language);
                torrentItem.SetString("AdditionalInfo", languageContent.AdditionalInfo, languageContent.Language);
                torrentItem.SetString("UrlName", torrentItem.Id.ToString(), languageContent.Language);
            }

            if (string.IsNullOrWhiteSpace(titleEn))
            {
                titleEn = torrentItem.Id.ToString();
            }
            torrentItem.SetValue(nameof(model.Genre), model.Genre);
            torrentItem.SetValue("DownloadLink", model.DownloadLink);
            torrentItem.SetValue("Owner", this.managerProvider.GetCurrentUserId());
            torrentItem.SetValue("PublicationDate", this.dateTimeProvider.UtcNow);

            Guid imageItemId = this.imagesService.CreateImage(model.Image, null, null);
            torrentItem.CreateRelation(imageItemId,"df", typeof(Image).FullName, "Image");

            torrentItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo(cultureName));
            versionManager.CreateVersion(torrentItem, false);
            this.managerProvider.CommitTransaction(transactionName);

            // Use lifecycle so that LanguageData and other Multilingual related values are correctly created
            DynamicContent checkOutTorrentItem = dynamicModuleManager.Lifecycle.CheckOut(torrentItem) as DynamicContent;
            DynamicContent checkInTorrentItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutTorrentItem) as DynamicContent;
            versionManager.CreateVersion(checkInTorrentItem, false);
            this.managerProvider.CommitTransaction(transactionName);
        }

        public IEnumerable<LanguageContents> GetAvailableLanguages()
        {
            //string providerName = "OpenAccessProvider";
            //string transactionName = "getTorrentAvailableLanguagesTransaction";
            //DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName);
            //Type torrentType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Torrents.Torrent");
            //DynamicContent torrentItem = dynamicModuleManager.GetDataItems(torrentType).FirstOrDefault();

            ICollection<LanguageContents> result = new List<LanguageContents>();
            foreach (var language in this.avaliableLanguages)
            {
                if (!string.IsNullOrWhiteSpace(language))
                {
                    result.Add(new LanguageContents
                    {
                        Language = language
                    });
                }
            }

            return result;
        }
    }
}