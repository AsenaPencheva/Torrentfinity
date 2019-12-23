namespace Torrentfinity.Sitefinity.Services.DynamicModules.Torrents
{
    using System.Collections.Generic;

    public interface IGenresService
    {
        IEnumerable<string> GetAll();

        //DynamicContent Get(string genre);

        //DynamicContent CreateGenre(string genre);
    }
}
