using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Backend.Models.Dto
{
    public class ContactDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } = string.Empty;
        

        public string? LastName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public string Category { get; set; } = string.Empty;

        public int UserId { get; set; }
    }
}
