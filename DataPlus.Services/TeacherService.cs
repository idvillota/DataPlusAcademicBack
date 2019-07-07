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
    public class TeacherService : BaseService, ITeacherService
    {
        public TeacherService(IWrapperRepository repositoryWrapper, ILoggerManager logger) :
             base(repositoryWrapper, logger)
        {
        }

        public IList<Teacher> GetAll()
        {
            try
            {
                return _wrapperRepository.Teacher.GetAll().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("TeacherService::GetAll::" + ex.Message);
                throw;
            }
        }

        public Teacher GetById(Guid teacherId)
        {
            try
            {

                return _wrapperRepository.Teacher.GetById(teacherId);
            }
            catch (Exception ex)
            {
                _logger.LogError("TeacherService::GetById::" + ex.Message);
                throw;
            }
        }

        //public TeacherExtended GetTeacherWithDetails(Guid teacherId)
        //{
        //    return _teacherRepository.GetTeacherWithDetails(teacherId);
        //}

        public void Create(Teacher teacher)
        {
            try
            {
                _wrapperRepository.Teacher.Create(teacher);
                _wrapperRepository.Teacher.SaveChanges();
                
            }
            catch (Exception ex)
            {
                _logger.LogError("TeacherService::Create::" + ex.Message);
                throw;
            }
        }

        public void Update(Teacher teacher)
        {
            try
            {
                _wrapperRepository.Teacher.Update(teacher);
                _wrapperRepository.Teacher.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError("TeacherService::Update::" + ex.Message);
                throw;
            }
        }

        public void Delete(Teacher teacher)
        {
            try
            {
                _wrapperRepository.Teacher.Delete(teacher);
                _wrapperRepository.Teacher.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError("TeacherService::Delete::" + ex.Message);
                throw;
            }
        }

    }
}
