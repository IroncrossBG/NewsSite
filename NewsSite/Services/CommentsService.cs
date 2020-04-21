using Microsoft.AspNetCore.Identity;
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
        public void Create(CreateEditCommentInputModel model)
        {
            var comment = new Comment
            {
                Content = model.Content,
                User = model.User,
                ArticleId = model.ArticleId,
                CreatedOn = DateTime.UtcNow
            };
            db.Comments.Add(comment);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var comment = this.db.Comments.FirstOrDefault(x => x.Id == id);
            if (comment != null)
            {
                this.db.Remove(comment);
                this.db.SaveChanges();
            }
        }
    }
}
