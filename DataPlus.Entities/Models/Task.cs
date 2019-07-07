using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataPlus.Entities.Models
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public ICollectionDocument> Documents { get; set; }
    }
}
