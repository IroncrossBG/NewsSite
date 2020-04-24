namespace NewsSite.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using NewsSite.Models.Input;

    public interface ICommentsService
    {
        Task CreateAsync(CreateEditCommentInputModel model);

        Task DeleteAsync(int id);
    }
}
