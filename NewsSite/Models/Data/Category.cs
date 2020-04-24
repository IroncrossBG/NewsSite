namespace NewsSite.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class Category
    {
        public Category()
        {
            this.Articles = new HashSet<Article>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
