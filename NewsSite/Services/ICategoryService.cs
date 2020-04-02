using NewsSite.Models.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Services
{
    public interface ICategoryService
    {
        Category GetById(int id);
    }
}
