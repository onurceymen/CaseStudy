using CaseStudyAdminMVC.Models;

namespace CaseStudyAdminMVC.ServicesAbstract
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentViewModel>> GetCommentsAsync();
        Task<bool> ApproveCommentAsync(int commentId);
    }
}
