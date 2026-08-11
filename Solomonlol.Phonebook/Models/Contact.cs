using Backend.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string PhoneNumber { get; set; } = string.Empty;
        public string Category {  get; set; } = string.Empty;
        
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        public Contact() { }

        public Contact(ContactDto dto)
        {
            FirstName = dto.FirstName;
            MiddleName = dto.MiddleName;
            LastName = dto.LastName;
            Email = dto.Email;
            PhoneNumber = dto.PhoneNumber;
            Category = dto.Category;
        }
    }
}
