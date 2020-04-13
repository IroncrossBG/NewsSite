using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Models.Data
{
    public class Article
    {
        public Article()
        {
            this.Comments = new HashSet<Comment>();
        }
        public int Id { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public int Views { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual IEnumerable<Comment> Comments { get; set; }
    }
}
