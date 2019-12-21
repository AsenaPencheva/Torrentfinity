namespace Torrentfinity.App_Start.NinjectModules
{
    using Ninject.Modules;
    using Torrentfinity.Sitefinity.Services.DynamicModules.Torrents;

    public class TorrentsModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            this.Bind<ITorrentsService>().To<TorrentsService>();
            this.Bind<IGenresService>().To<GenresService>();
        }
    }
}