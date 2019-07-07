using DataPlus.Contracts.Logger;
using DataPlus.Contracts.Repositories;
using DataPlus.Contracts.Services;
using DataPlus.Entities;
using DataPlus.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataPlus.Services
{
    public class SignatureService : BaseService, ISignatureService
    {
        public SignatureService(IWrapperRepository repositoryWrapper, ILoggerManager logger) :
            base(repositoryWrapper, logger)
        {
        }

        public IList<Signature> GetAll()
        {
            try
            {
                return _wrapperRepository.Signature.GetAll().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("SignatureService::GetAll::" + ex.Message);
                throw;
            }
        }

        public Signature GetById(Guid signatureId)
        {
            try
            {
                return _wrapperRepository.Signature.GetById(signatureId);
                
            }
            catch (Exception ex)
            {
                _logger.LogError("SignatureService::GetById::" + ex.Message);
                throw;
            }            
        }

        //public SignatureExtended GetSignatureWithDetails(Guid signatureId)
        //{
        //    return _signatureRepository.GetSignatureWithDetails(signatureId);
        //}

        public void Create(Signature signature)
        {
            try
            {
                _wrapperRepository.Signature.Create(signature);
                _wrapperRepository.Signature.SaveChanges();
             }
            catch (Exception ex)
            {
                _logger.LogError("SignatureService::Create::" + ex.Message);
                throw;
            }
        }

        public void Update(Signature signature)
        {
            try
            {
                _wrapperRepository.Signature.Update(signature);
                _wrapperRepository.Signature.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError("SignatureService::Update::" + ex.Message);
                throw;
            }
        }

        public void Delete(Signature signature)
        {   
            try
            {
                _wrapperRepository.Signature.Delete(signature);
                _wrapperRepository.Signature.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError("SignatureService::Delete::" + ex.Message);
                throw;
            }
        }
        
    }
}
