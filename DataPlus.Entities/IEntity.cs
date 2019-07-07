using System;
using System.Collections.Generic;
using System.Text;

namespace DataPlus.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
