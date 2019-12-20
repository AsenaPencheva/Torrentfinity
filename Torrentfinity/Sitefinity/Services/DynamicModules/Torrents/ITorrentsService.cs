namespace Torrentfinity.Sitefinity.Services.DynamicModules.Torrents
{
    using Torrentfinity.Mvc.Models;

    public interface ITorrentsService
    {
        void CreateTorrent(TorrentViewModel model);
    }
}