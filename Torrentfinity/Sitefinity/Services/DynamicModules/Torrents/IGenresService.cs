namespace Torrentfinity.Sitefinity.Services.DynamicModules.Torrents
{
    using System.Collections.Generic;
    using Telerik.Sitefinity.DynamicModules.Model;
    using Torrentfinity.Mvc.Models;

    public interface IGenresService
    {
        IEnumerable<GenreViewModel> GetAll();

        DynamicContent Get(string genre);

        DynamicContent CreateGenre(string genre);
    }
}
