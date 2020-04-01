using NewsSite.Models.Data;
using NewsSite.Models.Input;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Services
{
    public interface IArticlesService
    {
        Task AddArticle(AddArticleModel model);

        Article GetById(int id);
    }
}
