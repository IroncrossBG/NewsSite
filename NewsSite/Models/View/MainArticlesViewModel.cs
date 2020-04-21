using NewsSite.Models.Data;
using System.Collections.Generic;

namespace NewsSite.Models.View
{
    public class MainArticlesViewModel
    {
        public virtual List<Article> Articles { get; set; }
    }
}
