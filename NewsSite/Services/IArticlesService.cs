namespace NewsSite.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using NewsSite.Models.Data;
    using NewsSite.Models.Input;

    public interface IArticlesService
    {
        Task AddAsync(CreateEditArticleInputModel model);

        Task EditAsync(CreateEditArticleInputModel model);

        Task DeleteAsync(int id);

        Task IncreaseViewsAsync(int id);

        Article GetByIdAsync(int id);

        Task<List<Article>> GetAllAsync();
    }
}
