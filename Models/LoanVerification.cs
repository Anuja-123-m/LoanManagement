using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanManagementAPI.Models
{
    public class LoanVerification
    {
        [Key]
        public int VerificationId { get; set; }

        [Required]
        [StringLength(1000)]
        public string VerificationDetails { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // Pending, Verified, Rejected

        [StringLength(500)]
        public string? Remarks { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? VerifiedDate { get; set; }

        [ForeignKey("LoanRequest")]
        public int LoanRequestId { get; set; }
        public LoanRequest? LoanRequest { get; set; }

        [ForeignKey("LoanOfficer")]
        public int? AssignedOfficerId { get; set; }
        public LoanOfficer? AssignedOfficer { get; set; }
    }
}

