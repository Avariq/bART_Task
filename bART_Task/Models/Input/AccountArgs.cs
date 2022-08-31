using System.ComponentModel.DataAnnotations;

namespace bART_Task.API.Models.Input
{
    public class AccountArgs
    {
        [Required]
        [MinLength(1)]
        public string AccountName { get; set; }
    }
}
