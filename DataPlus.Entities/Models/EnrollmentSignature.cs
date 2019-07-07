using System;
using System.Collections.Generic;
using System.Text;

namespace DataPlus.Entities.Models
{
    public class EnrollmentSignature
    {
        public Guid EnrollmentId { get; set; }
        public Enrollment Enrollment { get; set; }
        public Guid SignatureId { get; set; }
        public Signature Signature { get; set; }
    }
}
