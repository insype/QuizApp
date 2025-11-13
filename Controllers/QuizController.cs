using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;

namespace QuizApp.Controllers
{
    [Authorize]
    public class QuizController : Controller
    {
        private readonly QuizDbContext _context;
        
        public QuizController(QuizDbContext context)
        {
            _context = context;
        }
        
        // Start Quiz
        public async Task<IActionResult> Index()
        {
            var questions = await _context.Questions.ToListAsync();
            return View(questions);
        }
        
        // Submit Quiz
        [HttpPost]
        public async Task<IActionResult> Submit(Dictionary<int, int> answers)
        {
            var questions = await _context.Questions.ToListAsync();
            int score = 0;
            
            foreach (var question in questions)
            {
                if (answers.ContainsKey(question.Id) && 
                    answers[question.Id] == question.CorrectOption)
                {
                    score++;
                }
            }
            
            ViewBag.Score = score;
            ViewBag.Total = questions.Count;
            ViewBag.Percentage = questions.Count > 0 
                ? (score * 100 / questions.Count) 
                : 0;
            
            return View("Result");
        }
    }
}