﻿namespace Torrentfinity.Mvc.Controllers
{
    using System.Web;
    using System.Web.Mvc;
    using Telerik.Microsoft.Practices.Unity.Utility;
    using Telerik.Sitefinity.Mvc;
    using Torrentfinity.Mvc.Models;
    using Torrentfinity.Sitefinity.Services.DynamicModules.Torrents;

    [ControllerToolboxItem(Name = "Torrents", SectionName = "MVC widgets", Title = "Torrents")]
    public class TorrentsController : Controller
    {
        private readonly ITorrentsService torrentsService;
        private readonly IGenresService genresService;

        public TorrentsController(ITorrentsService torrentsService, IGenresService genresService)
        {
            Guard.ArgumentNotNull(torrentsService, nameof(torrentsService));
            Guard.ArgumentNotNull(genresService, nameof(genresService));

            this.torrentsService = torrentsService;
            this.genresService = genresService;
        }

        // GET: TorrentWidget
        public ActionResult Index()
        {
            TorrentViewModel model = new TorrentViewModel
            {
                Genres = this.genresService.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(TorrentViewModel model, HttpPostedFileBase image)
        {
            this.torrentsService.CreateTorrent(model);

            return View("Index", new TorrentViewModel());
        }
    }
}