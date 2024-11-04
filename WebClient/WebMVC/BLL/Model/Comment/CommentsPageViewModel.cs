using BLL.Model.ModelRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Model.Comment
{
    public class CommentsPageViewModel
    {
        public AddCommentRequest AddCommentRequest { get; set; }
        public List<CommentDtos> Comments { get; set; }
    }
}
