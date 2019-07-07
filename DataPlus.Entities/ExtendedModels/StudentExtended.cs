using DataPlus.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataPlus.Entities.ExtendedModels
{
    public class StudentExtended : IEntity
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public StudentExtended()
        {
        }

        public StudentExtended(Student student)
        {
            Id = student.Id;
            FirstName = student.FirstName;
            LastName = student.LastName;
            Email = student.Email;
        }

    }
}
