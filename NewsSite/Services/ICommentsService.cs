using NewsSite.Models.Input;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Services
{
    public interface ICommentsService
    {
        Task CreateAsync(CreateEditCommentInputModel model);

        Task DeleteAsync(int id);
    }
}
