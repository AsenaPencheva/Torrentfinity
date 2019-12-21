namespace Torrentfinity.Sitefinity.Services.DynamicModules.Torrents
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;
    using Telerik.Sitefinity;
    using Telerik.Sitefinity.Data;
    using Telerik.Sitefinity.DynamicModules;
    using Telerik.Sitefinity.DynamicModules.Model;
    using Telerik.Sitefinity.Model;
    using Telerik.Sitefinity.Security;
    using Telerik.Sitefinity.Utilities.TypeConverters;
    using Telerik.Sitefinity.Versioning;
    using Torrentfinity.Mvc.Models;

    public class GenresService : IGenresService
    {
        public DynamicContent CreateGenre(string genre)
        {
            // Set the provider name for the DynamicModuleManager here. All available providers are listed in
            // Administration -> Settings -> Advanced -> DynamicModules -> Providers
            var providerName = "OpenAccessProvider";

            // Set a transaction name and get the version manager
            var transactionName = "createGenreTransaction";
            var versionManager = VersionManager.GetManager(null, transactionName);

            // Set the culture name for the multilingual fields
            var cultureName = "en";
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName);
            Type genreType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Torrents.Genre");
            DynamicContent genreItem = dynamicModuleManager.CreateDataItem(genreType);

            // This is how values for the properties are set
            genreItem.SetString("Name", genre, cultureName);


            genreItem.SetString("UrlName", "SomeUrlName", cultureName);
            genreItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            genreItem.SetValue("PublicationDate", DateTime.UtcNow);


            genreItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published", new CultureInfo(cultureName));

            // Create a version and commit the transaction in order changes to be persisted to data store
            versionManager.CreateVersion(genreItem, true);
            TransactionManager.CommitTransaction(transactionName);

            // Use lifecycle so that LanguageData and other Multilingual related values are correctly created
            DynamicContent checkOutGenreItem = dynamicModuleManager.Lifecycle.CheckOut(genreItem) as DynamicContent;
            DynamicContent checkInGenreItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutGenreItem) as DynamicContent;
            versionManager.CreateVersion(checkInGenreItem, false);
            TransactionManager.CommitTransaction(transactionName);

            return genreItem;
        }

        public DynamicContent Get(string genre)
        {
            return null;
        }

        public IEnumerable<GenreViewModel> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}