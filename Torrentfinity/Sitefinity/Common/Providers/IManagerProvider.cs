namespace Torrentfinity.Sitefinity.Common.Providers
{
    using System;
    using Telerik.Sitefinity.DynamicModules;
    using Telerik.Sitefinity.Versioning;

    public interface IManagerProvider
    {
        VersionManager GetVersionManager(string providerName, string transactionName);

        DynamicModuleManager GetDynamicModuleManager(string providerName, string transactionName);

        void CommitTransaction(string transactionName);

        Type ResolveType(string fullName);

        Guid GetCurrentUserId();
    }
}