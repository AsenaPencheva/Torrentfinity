namespace Torrentfinity.Sitefinity.Services.DynamicModules.Torrents
{
    using System.Collections.Generic;
    using Telerik.Sitefinity.DynamicModules.Model;
    using Torrentfinity.Mvc.Models;

    public interface IGenresService
    {
        IEnumerable<string> GetAll();

        DynamicContent Get(string genre);

        DynamicContent CreateGenre(string genre);
    }
}
