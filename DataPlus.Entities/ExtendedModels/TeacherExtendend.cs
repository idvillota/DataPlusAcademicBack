using DataPlus.Entities.Models;
using System;

namespace DataPlus.Entities.ExtendedModels
{
    public class TeacherExtended : IEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }        
        public string Address { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentType { get; set; }
        public string PhoneNumber { get; set; }

        public TeacherExtended()
        {
        }

        public TeacherExtended(Teacher teacher)
        {
            Id = teacher.Id;
            FirstName = teacher.FirstName;
            LastName = teacher.LastName;
            Email = teacher.Email;
        }

    }
}
