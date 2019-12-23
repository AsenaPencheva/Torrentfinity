namespace Torrentfinity.Sitefinity.Services.DynamicModules.Torrents
{
    using System.Collections.Generic;
    using Torrentfinity.Mvc.Models;

    public interface ITorrentsService
    {
        void CreateTorrent(TorrentViewModel model);

        IEnumerable<LanguageContents> GetAvailableLanguages();
    }
}