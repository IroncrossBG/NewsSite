using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Models.Input
{
    public class AddArticleModel
    {
        [Required, MinLength(3)]
        public string Title { get; set; }

        public string Subtitle { get; set; }

        [Required]
        public string Author { get; set; }

        [Required, MinLength(100)]
        public string Content { get; set; }
    }
}
