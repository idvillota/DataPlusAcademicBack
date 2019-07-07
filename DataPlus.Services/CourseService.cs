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
    public class CourseService : BaseService, ICourseService
    {
        public CourseService(IWrapperRepository repositoryWrapper, ILoggerManager logger) :
             base(repositoryWrapper, logger)
        {
        }

        public IList<Course> GetAll()
        {
            try
            {
                return _wrapperRepository.Course.GetAll().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("CourseService::GetAll::" + ex.Message);
                throw;
            }
        }

        public Course GetById(Guid courseId)
        {
            try
            {
                return _wrapperRepository.Course.GetById(courseId);
                
            }
            catch (Exception ex)
            {
                _logger.LogError("CourseService::GetById::" + ex.Message);
                throw;
            }            
        }

        //public CourseExtended GetCourseWithDetails(Guid courseId)
        //{
        //    return _courseRepository.GetCourseWithDetails(courseId);
        //}

        public void Create(Course course)
        {
            try
            {
                _wrapperRepository.Course.Create(course);
                _wrapperRepository.Course.SaveChanges();
             }
            catch (Exception ex)
            {
                _logger.LogError("CourseService::Create::" + ex.Message);
                throw;
            }
        }

        public void Update(Course course)
        {
            try
            {
                _wrapperRepository.Course.Update(course);
                _wrapperRepository.Course.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError("CourseService::Update::" + ex.Message);
                throw;
            }
        }

        public void Delete(Course course)
        {   
            try
            {
                _wrapperRepository.Course.Delete(course);
                _wrapperRepository.Course.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError("CourseService::Delete::" + ex.Message);
                throw;
            }
        }
        
    }
}
