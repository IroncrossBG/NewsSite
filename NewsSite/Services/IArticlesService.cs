using NewsSite.Models.Data;
using NewsSite.Models.Input;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Services
{
    public interface IArticlesService
    {
        void Add(CreateEditArticleInputModel model);

        void Edit(CreateEditArticleInputModel model);

        void Delete(int id);

        Article GetById(int id);

        IEnumerable<Article> GetAll();
    }
}
