using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Models.Data
{
    public class Vote
    {
        public int Id { get; set; }

        public bool Type { get; set; } //True = positive, False = negative

        public DateTime VotedOn { get; set; }

        public int CommentId { get; set; }

        public virtual Comment Comment { get; set; }

        public IdentityUser User { get; set; }
    }
}
