namespace Torrentfinity.Sitefinity.Services.DynamicModules.BuldInContents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;
    using Telerik.Sitefinity.Workflow;
    using Telerik.Sitefinity.Libraries.Model;
    using Telerik.Sitefinity.Modules.Libraries;

    public class ImagesService: IImagesService
    {
        public Image CreateImagettt(HttpPostedFileBase fileAttach, Guid? parentAlbumId, string title)
        {
            LibrariesManager librariesManager = LibrariesManager.GetManager();
            Image image = librariesManager.CreateImage();

            if (string.IsNullOrWhiteSpace(title))
            {
                image.Title = fileAttach.FileName;
            }
            else
            {
                image.Title = title;
            }

            Album album;
            if (!parentAlbumId.HasValue)
            {
                album = librariesManager.GetAlbums().FirstOrDefault();
            }
            else
            {
                album = librariesManager.GetAlbums().Where(i => i.Id == parentAlbumId).SingleOrDefault();
            }

            image.Parent = album;

            image.DateCreated = DateTime.UtcNow;
            image.PublicationDate = DateTime.UtcNow;
            image.LastModified = DateTime.UtcNow;
            image.UrlName = image.Id.ToString();
            image.MediaFileUrlName = Regex.Replace(fileAttach.FileName.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");

            string extension = "." + fileAttach.FileName.Split('.').Last();
            librariesManager.Upload(image, fileAttach.InputStream, extension);

            librariesManager.SaveChanges();

            var bag = new Dictionary<string, string>();
            bag.Add("ContentType", typeof(Image).FullName);
            WorkflowManager.MessageWorkflow(image.Id, typeof(Image), null, "Publish", false, bag);

            return image;
        }

        public Guid CreateImage(HttpPostedFileBase fileAttach, Guid? parentAlbumId, string title)
        {
            LibrariesManager librariesManager = LibrariesManager.GetManager();
            Image image = librariesManager.CreateImage();

            if (string.IsNullOrWhiteSpace(title))
            {
                image.Title = fileAttach.FileName;
            }
            else
            {
                image.Title = title;
            }

            Album album;
            if (parentAlbumId.HasValue)
            {
                album = librariesManager.GetAlbums().FirstOrDefault();
            }
            else
            {
                album = librariesManager.GetAlbums().Where(i => i.Id == parentAlbumId).SingleOrDefault();
            }

            image.Parent = album;

            image.DateCreated = DateTime.UtcNow;
            image.PublicationDate = DateTime.UtcNow;
            image.LastModified = DateTime.UtcNow;
            image.UrlName = image.Id.ToString();
            image.MediaFileUrlName = Regex.Replace(fileAttach.FileName.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");

            string extension = "." + fileAttach.FileName.Split('.').Last();
            librariesManager.Upload(image, fileAttach.InputStream, extension);

            librariesManager.SaveChanges();

            var bag = new Dictionary<string, string>();
            bag.Add("ContentType", typeof(Image).FullName);
            WorkflowManager.MessageWorkflow(image.Id, typeof(Image), null, "Publish", false, bag);

            return image.Id;
        }
    }
}