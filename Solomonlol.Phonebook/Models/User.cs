using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Backend.Models
{
    internal class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Login {  get; set; }
        public int PasswordHash { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string PhoneNumber { get; set; } = string.Empty;

        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();

        public User() { }

        public User(string login, string password, string firstName, string phoneNumber, string? lastName = "", string? email = "", string? middleName = "")
        {
            Login = login;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            PasswordHash = HashCode.Combine(phoneNumber, password);
        }
    }
}
