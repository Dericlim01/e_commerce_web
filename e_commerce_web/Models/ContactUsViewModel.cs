using System.ComponentModel.DataAnnotations;

namespace e_commerce_web.Models
{
    public class ContactUsViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        [StringLength(200)]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Message is required")]
        [StringLength(1000)]
        public string Message { get; set; }
    }
}
