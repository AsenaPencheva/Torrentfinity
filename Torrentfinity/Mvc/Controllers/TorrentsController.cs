namespace Torrentfinity.Mvc.Controllers
{
    using System.Web.Mvc;
    using Telerik.Sitefinity.Mvc;
    using Torrentfinity.Mvc.Models;
    using Torrentfinity.Sitefinity.Services.DynamicModules.Torrents;

    [ControllerToolboxItem(Name = "Torrents", SectionName = "MVC widgets", Title = "Torrents")]
    public class TorrentsController : Controller
    {
        private readonly ITorrentsService torrentsService;

        public TorrentsController(ITorrentsService torrentsService)
        {
            this.torrentsService = torrentsService;
        }

        // GET: TorrentWidget
        public ActionResult Index()
        {
            return View(new TorrentViewModel());
        }

        [HttpPost]
        public ActionResult Create(TorrentViewModel model)
        {
            this.torrentsService.CreateTorrent(model);

            return View("Index", new TorrentViewModel());
        }
    }
}