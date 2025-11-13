using Microsoft.EntityFrameworkCore;
using QuizApp.Models;
namespace QuizApp.Data
{
    public class QuizDbContext : DbContext
    {
        public QuizDbContext(DbContextOptions<QuizDbContext> options) 
            : base(options)
        {
        }
        
        public DbSet<Question> Questions { get; set; }
    }
}