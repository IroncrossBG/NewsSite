using NewsSite.Models.Input;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Services
{
    public interface ICommentsService
    {
        void Create(CreateEditCommentInputModel model);

        void Edit();

        void Delete();
    }
}
