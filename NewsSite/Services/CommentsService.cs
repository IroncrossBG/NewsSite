using Microsoft.AspNetCore.Identity;
using NewsSite.Data;
using NewsSite.Models.Data;
using NewsSite.Models.Input;
using System;
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

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Edit()
        {
            throw new NotImplementedException();
        }
    }
}
