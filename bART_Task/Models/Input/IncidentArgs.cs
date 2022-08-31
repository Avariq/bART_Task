using System.ComponentModel.DataAnnotations;

namespace bART_Task.API.Models.Input
{
    public class IncidentArgs
    {
        [Required]
        [MinLength(1)]
        public string Description { get; set; }

        [Required]
        public AccountArgs AccountArgs { get; set; }

        [Required]
        public ContactArgs ContactArgs { get; set; }
    }
}
