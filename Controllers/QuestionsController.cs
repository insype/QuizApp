using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly QuizDbContext _context;
        
        public QuestionsController(QuizDbContext context)
        {
            _context = context;
        }
        
        // List all questions
        public async Task<IActionResult> Index()
        {
            var questions = await _context.Questions.ToListAsync();
            return View(questions);
        }
        
        // Create - GET
        public IActionResult Create()
        {
            return View();
        }
        
        // Create - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Question question)
        {
            if (ModelState.IsValid)
            {
                _context.Questions.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(question);
        }
        
        // Edit - GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            
            var question = await _context.Questions.FindAsync(id);
            if (question == null) return NotFound();
            
            return View(question);
        }
        
        // Edit - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Question question)
        {
            if (id != question.Id) return NotFound();
            
            if (ModelState.IsValid)
            {
                _context.Update(question);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(question);
        }
        
        // Delete - GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            
            var question = await _context.Questions.FindAsync(id);
            if (question == null) return NotFound();
            
            return View(question);
        }
        
        // Delete - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}