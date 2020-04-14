using NewsSite.Models.Data;
using System.Collections.Generic;

namespace NewsSite.Models.View
{
    public class TopArticlesViewModel
    {
        public virtual IEnumerable<Article> TopRead { get; set; }

        public virtual IEnumerable<Article> TopCommented { get; set; }
    }
}
