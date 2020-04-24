namespace NewsSite.Models.Input
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;

    public class CreateEditCommentInputModel
    {
        public int Id { get; set; }
        
        public IdentityUser User { get; set; }

        public int ArticleId { get; set; }
        [Required, MinLength(250)]
        public string Content { get; set; }
    }
}
