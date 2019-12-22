using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Torrentfinity.Mvc.Models
{
    public class TorrentViewModel
    {
        public TorrentViewModel()
        {
            this.Genres = new List<string>();
        }

        [Required]
        public string Title { get; set; }

        public IEnumerable<string> Genres { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public string AdditionalInfo { get; set; }

        public string Description { get; set; }

        [Required]
        public string DownloadLink { get; set; }

        public HttpPostedFileBase Image { get; set; }
    }
}