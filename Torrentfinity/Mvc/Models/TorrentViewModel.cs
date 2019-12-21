namespace Torrentfinity.Mvc.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class TorrentViewModel
    {
        [Required]
        public string Title { get; set; }

        public IEnumerable<string> Genres { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public string AdditionalInfo { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string DownloadLink { get; set; }

        public HttpPostedFileBase FileAttach { get; set; }
    }
}