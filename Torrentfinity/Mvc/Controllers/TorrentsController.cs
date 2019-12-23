namespace Torrentfinity.Mvc.Controllers
{
    using System.Globalization;
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

        public ActionResult Index()
        { 
            TorrentViewModel model = new TorrentViewModel
            {
                Genres = this.genresService.GetAll(),
                LanguageContents= this.torrentsService.GetAvailableLanguages()
            };

            return this.View(model);
        }

        [HttpPost]
        public ActionResult Create(TorrentViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Genres = this.genresService.GetAll();
                model.LanguageContents = this.torrentsService.GetAvailableLanguages();
                return this.View("Index", model);
            }

            try
            {
                this.torrentsService.CreateTorrent(model);
            }
            catch (System.Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                model.Genres = this.genresService.GetAll();
                model.LanguageContents = this.torrentsService.GetAvailableLanguages();

                return this.View("Index", model);
            }

            return this.RedirectToAction("Index");
        }
    }
}