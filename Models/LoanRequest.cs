using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanManagementAPI.Models
{
    public class LoanRequest
    {
        [Key]
        public int LoanRequestId { get; set; }

        [Required]
        [StringLength(50)]
        public string LoanType { get; set; } = string.Empty; // Education, Home, Car, etc.

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal LoanAmount { get; set; }

        [Required]
        public int TenureMonths { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal InterestRate { get; set; }

        [Required]
        [StringLength(1000)]
        public string Purpose { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected, UnderVerification

        public DateTime RequestDate { get; set; } = DateTime.Now;

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [ForeignKey("LoanOfficer")]
        public int? AssignedOfficerId { get; set; }
        public LoanOfficer? AssignedOfficer { get; set; }
    }
}

