using System.ComponentModel.DataAnnotations;

namespace ShiftLogger.Backend.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public ICollection<Shift> Shifts { get; set; } = new List<Shift>();
    }
}
