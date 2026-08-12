using System.ComponentModel.DataAnnotations;

namespace e_commerce_web.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        [Display(Name = "Member Since")]
        public DateTime MemberSince { get; set; } = DateTime.Now;
    }
}
