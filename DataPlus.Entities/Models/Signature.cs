using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataPlus.Entities.Models
{
    public class Signature : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<EnrollmentSignature> EnrollmentSignatures { get; set; }
    }
}
