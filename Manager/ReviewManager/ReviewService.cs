using Microsoft.EntityFrameworkCore;
using ZalakProject.Data;
using ZalakProject.Models;
using ZalakProject.ViewModels;

namespace ZalakProject.Manager.ReviewManager
{
    public class ReviewService:IReviewService
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Review> _reviewsDao; 
        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
            _reviewsDao = _context.Reviews;
        }

        public async Task AddReview(ReviewViewModel model, string userId)
        {
            var review = new Review
            {
                ProductId = model.ProductId,
                UserId = userId,
                Rating = model.Rating,
                Comment = model.Comment,
                CreatedAt = DateTime.Now
            };

            _reviewsDao.Add(review);
            await _context.SaveChangesAsync();
        }
    }
}
