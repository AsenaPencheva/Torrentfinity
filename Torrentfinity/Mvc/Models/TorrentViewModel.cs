namespace Torrentfinity.Mvc.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class TorrentViewModel
    {
        public TorrentViewModel()
        {
            this.Genres = new List<string>();
            this.LanguageContents = new List<LanguageContents>();
        }

        public IEnumerable<string> Genres { get; set; }

        public IEnumerable<LanguageContents> LanguageContents { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public string DownloadLink { get; set; }

        public HttpPostedFileBase Image { get; set; }
    }
}