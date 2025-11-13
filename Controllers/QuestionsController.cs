using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    [Authorize]
    public class QuestionsController : Controller
    {
        private readonly QuizDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public QuestionsController(
            QuizDbContext context, 
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        // Check if user is admin
        private async Task<bool> IsAdmin()
        {
            var user = await _userManager.GetUserAsync(User);
            return user?.IsAdmin ?? false;
        }
        
        // List all questions (Admin only)
        public async Task<IActionResult> Index()
        {
            if (!await IsAdmin())
            {
                return RedirectToAction("Index", "Quiz");
            }
            
            var questions = await _context.Questions.ToListAsync();
            return View(questions);
        }
        
        // Create - GET (Admin only)
        public async Task<IActionResult> Create()
        {
            if (!await IsAdmin())
            {
                return RedirectToAction("Index", "Quiz");
            }
            return View();
        }
        
        // Create - POST (Admin only)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Question question)
        {
            if (!await IsAdmin())
            {
                return RedirectToAction("Index", "Quiz");
            }
            
            if (ModelState.IsValid)
            {
                _context.Questions.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(question);
        }
        
        // Edit - GET (Admin only)
        public async Task<IActionResult> Edit(int? id)
        {
            if (!await IsAdmin())
            {
                return RedirectToAction("Index", "Quiz");
            }
            
            if (id == null) return NotFound();
            
            var question = await _context.Questions.FindAsync(id);
            if (question == null) return NotFound();
            
            return View(question);
        }
        
        // Edit - POST (Admin only)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Question question)
        {
            if (!await IsAdmin())
            {
                return RedirectToAction("Index", "Quiz");
            }
            
            if (id != question.Id) return NotFound();
            
            if (ModelState.IsValid)
            {
                _context.Update(question);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(question);
        }
        
        // Delete - GET (Admin only)
        public async Task<IActionResult> Delete(int? id)
        {
            if (!await IsAdmin())
            {
                return RedirectToAction("Index", "Quiz");
            }
            
            if (id == null) return NotFound();
            
            var question = await _context.Questions.FindAsync(id);
            if (question == null) return NotFound();
            
            return View(question);
        }
        
        // Delete - POST (Admin only)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!await IsAdmin())
            {
                return RedirectToAction("Index", "Quiz");
            }
            
            var question = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}