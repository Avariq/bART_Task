using System.ComponentModel.DataAnnotations;

namespace bART_Task.API.Models.Input
{
    public class ContactArgs
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]+$", ErrorMessage = "Such characters are not allowed.")]
        [MinLength(1)]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]+$", ErrorMessage = "Such characters are not allowed.")]
        [MinLength(1)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
