using System;
using System.ComponentModel.DataAnnotations;

namespace DataPlus.Entities.Models
{
    public class Person : IEntity
    {
        public Guid Id { get; set; }

        public EDocumentType DocumentType { get; set; }

        [Required(ErrorMessage = "Document Number is required")]
        [StringLength(15, ErrorMessage = "Document Number can't be longer than 15 characters")]
        public string DocumentNumber { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(60, ErrorMessage = "Last Name can't be longer than 60 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "Address can't be longer than 15 characters")]
        public string Address { get; set; }

        [StringLength(15, ErrorMessage = "Phone Number can't be longer than 15 characters")]
        public string PhoneNumber { get; set; }

        [StringLength(50, ErrorMessage = "City can't be longer than 50 characters")]
        public string City { get; set; }

        public DateTime Birth { get; set; }
    }
}
