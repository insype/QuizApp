using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class Question
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(500)]
        public string QuestionText { get; set; }
        
        [Required]
        public string Option1 { get; set; }
        
        [Required]
        public string Option2 { get; set; }
        
        [Required]
        public string Option3 { get; set; }
        
        [Required]
        public string Option4 { get; set; }
        
        [Required]
        [Range(1, 4)]
        public int CorrectOption { get; set; }
    }
}
