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
    public class SignatureRepository : RepositoryBase<Signature>, ISignatureRepository
    {
        public SignatureRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }


    }
}
