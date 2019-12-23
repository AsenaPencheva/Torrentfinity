namespace Torrentfinity.Mvc.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LanguageContents
    {
        public string Language { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string AdditionalInfo { get; set; }

        public string Description { get; set; }
    }
}