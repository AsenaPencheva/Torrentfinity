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
        }

        [Required]
        public string TitleEn { get; set; }

        [Required]
        public string TitleBg { get; set; }

        public IEnumerable<string> Genres { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public string AdditionalInfoBg { get; set; }

        [Required]
        public string AdditionalInfoEn { get; set; }

        public string DescriptionBg { get; set; }

        public string DescriptionEn { get; set; }

        [Required]
        public string DownloadLink { get; set; }

        public HttpPostedFileBase Image { get; set; }
    }
}