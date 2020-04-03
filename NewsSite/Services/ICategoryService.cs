using NewsSite.Models.Data;
using NewsSite.Models.Input;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Services
{
    public interface ICategoryService
    {
        void Add(CreateEditCategoryInputModel model);

        void Edit(CreateEditCategoryInputModel model);

        Category GetById(int id);

        IEnumerable<Category> GetAll();
    }
}
