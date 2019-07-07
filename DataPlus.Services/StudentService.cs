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
    public class StudentService : BaseService, IStudentService
    {
        public StudentService(IWrapperRepository repositoryWrapper, ILoggerManager logger): 
            base(repositoryWrapper, logger)
        {
        }

        public IList<Student> GetAll()
        {
            try
            {
                return _wrapperRepository.Student.GetAll().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("StudentService::GetAll::" + ex.Message);
                throw;
            }
        }

        public Student GetById(Guid studentId)
        {
            try
            {
                return _wrapperRepository.Student.GetById(studentId);
                
            }
            catch (Exception ex)
            {
                _logger.LogError("StudentService::GetById::" + ex.Message);
                throw;
            }            
        }

        //public StudentExtended GetStudentWithDetails(Guid studentId)
        //{
        //    return _studentRepository.GetStudentWithDetails(studentId);
        //}

        public void Create(Student student)
        {
            try
            {
                _wrapperRepository.Student.Create(student);
                _wrapperRepository.Student.SaveChanges();
             }
            catch (Exception ex)
            {
                _logger.LogError("StudentService::Create::" + ex.Message);
                throw;
            }
        }

        public void Update(Student student)
        {
            try
            {
                _wrapperRepository.Student.Update(student);
                _wrapperRepository.Student.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError("StudentService::Update::" + ex.Message);
                throw;
            }
        }

        public void Delete(Student student)
        {   
            try
            {
                _wrapperRepository.Student.Delete(student);
                _wrapperRepository.Student.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError("StudentService::Delete::" + ex.Message);
                throw;
            }
        }
        
    }
}
