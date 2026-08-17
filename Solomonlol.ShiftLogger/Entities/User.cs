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

        [Required(ErrorMessage ="User Identification number not specified")]
        [Range(1, 999999)]
        public int UserIdentificationNumber { get; set; }

        [JsonIgnore]
        public ICollection<Shift> Shifts { get; set; } = new List<Shift>();
    }
}
