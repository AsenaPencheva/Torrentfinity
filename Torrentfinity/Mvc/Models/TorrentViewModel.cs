using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Torrentfinity.Mvc.Models
{
    public class TorrentViewModel
    {
        public string Title { get; set; }
        public GenreViewModel Genre { get; set; }

        public string AdditionalInfo { get; set; }

        public string Description { get; set; }
        
        public string DownloadLink { get; set; }

       // Image
    }
}