using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Services
{
    public interface IVotesService
    {
        void UpVote();

        void DownVote();

        void Delete();
    }
}
