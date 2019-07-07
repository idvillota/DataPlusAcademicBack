using DataPlus.Contracts.Logger;
using DataPlus.Contracts.Repositories;
using DataPlus.Contracts.Services;
using DataPlus.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataPlus.Services
{
    public class EnrollmentService : BaseService, IEnrollmentService
    {
        public EnrollmentService(IWrapperRepository repositoryWrapper, ILoggerManager logger) :
            base(repositoryWrapper, logger)
        {
        }

        public void Create(Enrollment entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Enrollment entity)
        {
            throw new NotImplementedException();
        }

        public IList<Enrollment> GetAll()
        {
            throw new NotImplementedException();
        }

        public Enrollment GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Enrollment entity)
        {
            throw new NotImplementedException();
        }
    }
}
