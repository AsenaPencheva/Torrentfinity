namespace Torrentfinity.Sitefinity.Common.Providers
{
    using System;
    using Telerik.Sitefinity.Data;
    using Telerik.Sitefinity.DynamicModules;
    using Telerik.Sitefinity.Security;
    using Telerik.Sitefinity.Utilities.TypeConverters;
    using Telerik.Sitefinity.Versioning;

    public class ManagerProvider : IManagerProvider
    {
        public void CommitTransaction(string transactionName)
        {
            TransactionManager.CommitTransaction(transactionName);
        }

        public Guid GetCurrentUserId()
        {
            return SecurityManager.GetCurrentUserId();
        }

        public DynamicModuleManager GetDynamicModuleManager(string providerName, string transactionName)
        {
            return DynamicModuleManager.GetManager(providerName, transactionName);
        }

        public VersionManager GetVersionManager(string providerName, string transactionName)
        {
            return VersionManager.GetManager(providerName, transactionName);
        }

        public Type ResolveType(string fullName)
        {
            return TypeResolutionService.ResolveType(fullName);
        }
    }
}