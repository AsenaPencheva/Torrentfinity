namespace Torrentfinity.Sitefinity.Services.DynamicModules.Torrents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Telerik.Sitefinity.Data.Linq.Dynamic;
    using Telerik.Sitefinity.DynamicModules;
    using Telerik.Sitefinity.Model;
    using Telerik.Sitefinity.Utilities.TypeConverters;

    public class GenresService : IGenresService
    {

        public IEnumerable<string> GetAll()
        {
            var providerName = "OpenAccessProvider";
            var transactionName = "getGenresTransaction";

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName);
            Type genreType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Torrents.Genre");
            
            //  CreateGenreItem(dynamicModuleManager, genreType, transactionName);
            //var x = dynamicModuleManager.GetDataItems(genreType).ToList();
            // This is how we get the collection of Genre items
            var myCollection = dynamicModuleManager.GetDataItems(genreType).ToList().Where(x => !x.IsDeleted).Select(x => x.GetString("Name").Value).Distinct().ToList();
            // At this point myCollection contains the items from type genreType
            return myCollection;
        }

        //// Creates a new genre item
        //private void CreateGenreItem(DynamicModuleManager dynamicModuleManager, Type genreType, string transactionName)
        //{
        //    // Set the culture name for the multilingual fields
        //    var cultureName = "en";
        //    Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);

        //    DynamicContent genreItem = dynamicModuleManager.CreateDataItem(genreType);

        //    // This is how values for the properties are set 
        //    genreItem.SetString("Name", "Some Name", cultureName);

        //    genreItem.SetString("UrlName", "SomeUrlName", cultureName);
        //    genreItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
        //    genreItem.SetValue("PublicationDate", DateTime.UtcNow);

        //    genreItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft", new CultureInfo(cultureName));

        //    // Create a version and commit the transaction in order changes to be persisted to data store
        //    var versionManager = VersionManager.GetManager(null, transactionName);
        //    versionManager.CreateVersion(genreItem, false);
        //    TransactionManager.CommitTransaction(transactionName);

        //    // Use lifecycle so that LanguageData and other Multilingual related values are correctly created
        //    DynamicContent checkOutGenreItem = dynamicModuleManager.Lifecycle.CheckOut(genreItem) as DynamicContent;
        //    DynamicContent checkInGenreItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutGenreItem) as DynamicContent;
        //    versionManager.CreateVersion(checkInGenreItem, false);
        //    TransactionManager.CommitTransaction(transactionName);
        //}

        // not needed for now
        //    public DynamicContent CreateGenre(string genre)
        //    {
        //        var providerName = "OpenAccessProvider";
        //        var transactionName = "createGenreTransaction";
        //        var versionManager = VersionManager.GetManager(null, transactionName);

        //        // Set the culture name for the multilingual fields
        //        var cultureName = "en";
        //        Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);

        //        DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName);
        //        Type genreType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Torrents.Genre");
        //        DynamicContent genreItem = dynamicModuleManager.CreateDataItem(genreType);

        //        genreItem.SetString("Name", genre, cultureName);

        //        genreItem.SetString("UrlName", "SomeUrlName", cultureName);
        //        genreItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
        //        genreItem.SetValue("PublicationDate", DateTime.UtcNow);

        //        genreItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published", new CultureInfo(cultureName));

        //        versionManager.CreateVersion(genreItem, true);
        //        TransactionManager.CommitTransaction(transactionName);

        //        // Use lifecycle so that LanguageData and other Multilingual related values are correctly created
        //        DynamicContent checkOutGenreItem = dynamicModuleManager.Lifecycle.CheckOut(genreItem) as DynamicContent;
        //        DynamicContent checkInGenreItem = dynamicModuleManager.Lifecycle.CheckIn(checkOutGenreItem) as DynamicContent;
        //        versionManager.CreateVersion(checkInGenreItem, false);
        //        TransactionManager.CommitTransaction(transactionName);

        //        return genreItem;
        //    }

        //    public DynamicContent Get(string genre)
        //    {
        //        // Set the provider name for the DynamicModuleManager here. All available providers are listed in
        //        // Administration -> Settings -> Advanced -> DynamicModules -> Providers
        //        var providerName = String.Empty;

        //        // Set a transaction name
        //        var transactionName = "someTransactionName";


        //        // Set the culture name for the multilingual fields
        //        var cultureName = "en";
        //        Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);

        //        DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName);
        //        Type genreType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Torrents.Genre");
        //        // CreateGenreItem(dynamicModuleManager, genreType, cultureName, transactionName);

        //        // This is how we get the genre items through filtering
        //        var myFilteredCollection = dynamicModuleManager.GetDataItems(genreType).Where($"Name = \"{genre}\"").FirstOrDefault();
        //        // At this point myFilteredCollection contains the items that match the lambda expression passed to the Where extension method
        //        // If you want only the first matching element you can freely get it by ".First()" extension method like this:
        //        // var myFirstFilteredItem = myFilteredCollection.First();
        //        return myFilteredCollection;
        //    }
    }
}