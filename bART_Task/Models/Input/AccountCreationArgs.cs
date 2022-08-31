using System.ComponentModel.DataAnnotations;

namespace bART_Task.API.Models.Input
{
    public class AccountCreationArgs
    {
        [Required]
        [MinLength(1)]
        public string AccountName { get; set; }

        [Required]
        [EmailAddress]
        public string ContactEmail { get; set; }
    }
}
