namespace Torrentfinity.Sitefinity.Services.DynamicModules.BuldInContents
{
    using System;
    using System.Web;

    public interface IImagesService
    {
        Guid CreateImage(HttpPostedFileBase fileAttach, Guid? parentAlbumId, string title);
    }
}