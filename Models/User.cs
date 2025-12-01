using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LoanManagementAPI.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = string.Empty; // Admin, Customer, LoanOfficer

        public bool IsApproved { get; set; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}

