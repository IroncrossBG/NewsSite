using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Models.Input
{
    public class CreateEditCommentInputModel
    {
        public int Id { get; set; }
        
        public IdentityUser User { get; set; }

        public int ArticleId { get; set; }
        [Required, MinLength(250)]
        public string Content { get; set; }
    }
}
