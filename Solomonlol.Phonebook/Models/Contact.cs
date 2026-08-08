using Backend.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    internal class Contact
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

        public Contact(string firstName, string phoneNumber, string category, int userId, string? middleName = "", string? lastName="", string? email="")
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Category = category;
            UserId = userId;
        }
    }
}
