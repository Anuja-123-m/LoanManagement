using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanManagementAPI.Models
{
    public class BackgroundVerification
    {
        [Key]
        public int VerificationId { get; set; }

        [Required]
        [StringLength(50)]
        public string VerificationType { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Details { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // Pending, Completed, Failed

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? CompletedDate { get; set; }

        [ForeignKey("LoanRequest")]
        public int LoanRequestId { get; set; }
        public LoanRequest? LoanRequest { get; set; }

        [ForeignKey("LoanOfficer")]
        public int? AssignedOfficerId { get; set; }
        public LoanOfficer? AssignedOfficer { get; set; }
    }
}

