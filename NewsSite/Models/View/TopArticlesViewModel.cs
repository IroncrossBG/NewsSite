namespace NewsSite.Models.View
{
    using System.Collections.Generic;
    using NewsSite.Models.Data;

    public class TopArticlesViewModel
    {
        public virtual IEnumerable<Article> TopRead { get; set; }

        public virtual IEnumerable<Article> TopCommented { get; set; }
    }
}
