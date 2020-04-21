using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsSite.Data;
using NewsSite.Models.Data;
using NewsSite.Models.Input;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly ApplicationDbContext db;
        public CommentsService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task CreateAsync(CreateEditCommentInputModel model)
        {
            var comment = new Comment
            {
                Content = model.Content,
                User = model.User,
                ArticleId = model.ArticleId,
                CreatedOn = DateTime.UtcNow
            };
            await db.Comments.AddAsync(comment);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var comment = await this.db.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (comment != null)
            {
                this.db.Remove(comment);
                await this.db.SaveChangesAsync();
            }
        }
    }
}
