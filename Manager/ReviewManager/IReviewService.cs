using ZalakProject.ViewModels;

namespace ZalakProject.Manager.ReviewManager
{
    public interface IReviewService
    {
        Task AddReview(ReviewViewModel model, string userId);
    }
}
