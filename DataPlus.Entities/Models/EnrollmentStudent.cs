using System;
using System.Collections.Generic;
using System.Text;

namespace DataPlus.Entities.Models
{
    public class EnrollmentStudent
    {
        public Guid EnrollmentId { get; set; }
        public Enrollment Enrollment { get; set; }
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
    }
}
