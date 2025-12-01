using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanManagementAPI.Models
{
    public class CustomerFeedback
    {
        [Key]
        public int FeedbackId { get; set; }

        [Required]
        [StringLength(2000)]
        public string FeedbackText { get; set; } = string.Empty;

        public int? Rating { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [ForeignKey("FeedbackQuestion")]
        public int? QuestionId { get; set; }
        public FeedbackQuestion? FeedbackQuestion { get; set; }
    }
}

