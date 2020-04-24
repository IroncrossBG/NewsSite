namespace NewsSite.Models.View
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using NewsSite.Models.Data;

    public class LastArticlesViewModel
    {
        public string Title { get; set; }
        public virtual IEnumerable<Article> Articles { get; set; }
    }
}
