using System.ComponentModel.DataAnnotations;

namespace LoanManagementAPI.Models
{
    public class FeedbackQuestion
    {
        [Key]
        public int QuestionId { get; set; }

        [Required]
        [StringLength(500)]
        public string QuestionText { get; set; } = string.Empty;

        [StringLength(50)]
        public string QuestionType { get; set; } = "Text"; // Text, Rating, YesNo

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? UpdatedDate { get; set; }
    }
}

