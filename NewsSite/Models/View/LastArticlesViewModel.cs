using NewsSite.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Models.View
{
    public class LastArticlesViewModel
    {
        public string Title { get; set; }
        public virtual IEnumerable<Article> Articles { get; set; }
    }
}
