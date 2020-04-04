﻿using NewsSite.Models.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewsSite.Models.Input
{
    public class CreateEditArticleInputModel
    {
        public int Id { get; set; }

        [Required, MinLength(3)]
        public string Title { get; set; }

        public string Subtitle { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Content { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
