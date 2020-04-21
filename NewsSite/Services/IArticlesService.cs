using NewsSite.Models.Data;
using NewsSite.Models.Input;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Services
{
    public interface IArticlesService
    {
        Task AddAsync(CreateEditArticleInputModel model);

        Task EditAsync(CreateEditArticleInputModel model);

        Task DeleteAsync(int id);

        Task IncreaseViewsAsync(int id);

        Task<Article> GetByIdAsync(int id);

        Task<List<Article>> GetAllAsync();
    }
}
