using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Models.Data
{
    public class Comment
    {
        public Comment()
        {
            User = new IdentityUser();
        }
        public int Id { get; set; }

        public string Content { get; set; }

        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }

        public virtual IdentityUser User { get; set; }

        public virtual IEnumerable<Vote> Votes { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
