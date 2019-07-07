using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataPlus.Entities.Models
{
    public class Course : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }    
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
