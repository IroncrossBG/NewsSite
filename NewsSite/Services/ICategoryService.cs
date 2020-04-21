using NewsSite.Models.Data;
using NewsSite.Models.Input;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Services
{
    public interface ICategoryService
    {

        Category GetById(int id, bool returnArticles);

        Category GetByName(string name, bool returnArticles);

        IEnumerable<Category> GetAll();
    }
}
