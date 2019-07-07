using System;
using System.Collections.Generic;
using System.Text;

namespace DataPlus.Entities.Models
{
    public class Enrollment : IEntity
    {
        public Guid Id { get; set; }
        public Course Course { get; set; }
        public ICollection<EnrollmentSignature> EnrollmentSignatures { get; set; }
        public ICollection<EnrollmentStudent> EnrollmentStudents { get; set; }
        public EEnrollmentStatus EnrollmentStatus { get; set; }
    }
}
