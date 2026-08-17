using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShiftLogger.Backend.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name not specified")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name not specified")]
        [StringLength(50)]
        public string LastName { get; set; }

        
        [Range(1, 999999, ErrorMessage ="User identification number should be on 1-999999 range")]
        public int UserIdentificationNumber { get; set; }

        [JsonIgnore]
        public ICollection<Shift> Shifts { get; set; } = new List<Shift>();
    }
}
