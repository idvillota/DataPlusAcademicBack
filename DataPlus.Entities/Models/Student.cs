using System.Collections.Generic;

namespace DataPlus.Entities.Models
{
    public class Student : Person
    {
        public ICollection<EnrollmentStudent> EnrollmentStudents { get; set; }
    }
}
