using SigortaApp.BL.Abstract;
using SigortaApp.Entity.Concrete;
using SigortaApp.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigortaApp.BL.Concrete
{
    public class CommentService : ICommentService
    {
        ICommentRepository _commentDal;

        public CommentService(ICommentRepository commentDal)
        {
            _commentDal = commentDal;
        }

        public void AddComment(Comment comment)
        {
            _commentDal.Insert(comment);
        }

        public List<Comment> GetList(int id)
        {
            return _commentDal.GetListAll(x => x.BlogId == id);
        }

        public List<Comment> GetListWithBlog()
        {
            return _commentDal.GetListWithBlog();
        }
    }
}
