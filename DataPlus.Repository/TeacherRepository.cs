using DataPlus.Contracts.Repositories;
using DataPlus.Entities;
using DataPlus.Entities.ExtendedModels;
using DataPlus.Entities.Extensions;
using DataPlus.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataPlus.Repository
{
    public class TeacherRepository : RepositoryBase<Teacher>, ITeacherRepository
    {
        public TeacherRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }


    }
}
