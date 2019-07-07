using DataPlus.Contracts.Logger;
using DataPlus.Contracts.Services;
using DataPlus.Entities.Extensions;
using DataPlus.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DataPlus.Academic.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private ILoggerManager _logger;
        private ICourseService _courseService;

        public CourseController(ILoggerManager logger, ICourseService courseService)
        {
            _logger = logger;
            _courseService = courseService;
        }
        
        // GET: api/Course
        [HttpGet]
        [ProducesResponseType(typeof(IList<Course>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public IActionResult GetAllCourses()
        {
            try
            {
                var courses = _courseService.GetAll();
                _logger.LogInfo($"Returned all courses from database.");
                return Ok(courses);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllCourses action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get courses filter by Id
        /// </summary>
        /// <param name="id">Course id</param>
        [HttpGet("{id}", Name = "CourseById")]
        [ProducesResponseType(typeof(Course), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public IActionResult GetCourseById(Guid id)
        {
            try
            {
                var course = _courseService.GetById(id);
                if (course.IsEmptyObject())
                {
                    _logger.LogError($"Course with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned course with id: {id}");
                    return Ok(course);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetCourseById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //[HttpGet("{id}/account")]
        //public IActionResult GetCourseWithDetails(Guid id)
        //{
        //    try
        //    {
        //        var course = _courseService.GetCourseWithDetails(id);

        //        if (course.Id.Equals(Guid.Empty))
        //        {
        //            _logger.LogError($"Course with id: {id}, hasn't been found in db.");
        //            return NotFound();
        //        }
        //        else
        //        {
        //            _logger.LogInfo($"Returned course with details for id: {id}");
        //            return Ok(course);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Something went wrong inside GetCourseWithDetails action: {ex.Message}");
        //        return StatusCode(500, "Internal server error");
        //    }
        //}

        /// <summary>
        /// Allow create a course
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Course), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public IActionResult CreateCourse([FromBody]Course course)
        {
            try
            {
                if (course.IsObjectNull())
                {
                    _logger.LogError("Course object sent from client is null.");
                    return BadRequest("Course object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid course object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _courseService.Create(course);
                return CreatedAtRoute("CourseById", new { id = course.Id }, course);
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateCourse action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Allow update a course
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Course), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public IActionResult UpdateCourse(Guid id, [FromBody]Course course)
        {
            try
            {
                if (course.IsObjectNull())
                {
                    _logger.LogError("Course object sent from client is null.");
                    return BadRequest("Course object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid course object sent from client.");
                    return BadRequest("Invalid model object");
                }
                
                _courseService.Update(course);

                return NoContent();
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateCourse action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Allow delete a course
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Course), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public IActionResult DeleteCourse(Guid CourseId)
        {
            try
            {
                if (CourseId == null || CourseId == Guid.Empty )                
                {
                    _logger.LogError("Course id null or empty");
                    return BadRequest("Course id is null or empty");
                }

                var dbCourse = _courseService.GetById(CourseId);
                if (dbCourse.IsEmptyObject())
                {
                    _logger.LogError($"Course with id: {CourseId}, hasn't been found in db.");
                    return NotFound();
                }

                _courseService.Delete(dbCourse);

                return NoContent();
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteCourse action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
