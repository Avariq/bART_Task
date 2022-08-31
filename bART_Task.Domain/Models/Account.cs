using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bART_Task.Domain.Models
{
    public class Account
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? IncidentName { get; set; }

        public virtual Incident? Incident { get; set; }

        public virtual ICollection<Contact>? Contacts { get; set; }
    }
}
