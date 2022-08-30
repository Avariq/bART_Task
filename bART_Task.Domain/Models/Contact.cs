using System.ComponentModel.DataAnnotations;

namespace bART_Task.Domain.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public int AccountId { get; set; }

        public virtual Account Account { get; set; }
    }
}
