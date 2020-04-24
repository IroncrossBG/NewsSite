namespace NewsSite.Models.View
{
    using System.Collections.Generic;
    using NewsSite.Models.Data;

    public class MainArticlesViewModel
    {
        public virtual List<Article> Articles { get; set; }
    }
}
