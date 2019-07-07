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
    public class TeacherController : ControllerBase
    {
        private ILoggerManager _logger;
        private ITeacherService _teacherService;

        public TeacherController(ILoggerManager logger, ITeacherService teacherService)
        {
            _logger = logger;
            _teacherService = teacherService;
        }

        // GET: api/Teacher
        [HttpGet]
        [ProducesResponseType(typeof(IList<Teacher>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public IActionResult GetAllTeachers()
        {
            try
            {
                var teachers = _teacherService.GetAll();
                _logger.LogInfo($"Returned all teachers from database.");
                return Ok(teachers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllTeachers action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get teachers filter by Id
        /// </summary>
        /// <param name="id">Teacher id</param>
        [HttpGet("{id}", Name = "TeacherById")]
        [ProducesResponseType(typeof(Teacher), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public IActionResult GetTeacherById(Guid id)
        {
            try
            {
                var teacher = _teacherService.GetById(id);
                if (teacher.IsEmptyObject())
                {
                    _logger.LogError($"Teacher with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned teacher with id: {id}");
                    return Ok(teacher);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetTeacherById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //[HttpGet("{id}/account")]
        //public IActionResult GetTeacherWithDetails(Guid id)
        //{
        //    try
        //    {
        //        var teacher = _teacherService.GetTeacherWithDetails(id);

        //        if (teacher.Id.Equals(Guid.Empty))
        //        {
        //            _logger.LogError($"Teacher with id: {id}, hasn't been found in db.");
        //            return NotFound();
        //        }
        //        else
        //        {
        //            _logger.LogInfo($"Returned teacher with details for id: {id}");
        //            return Ok(teacher);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Something went wrong inside GetTeacherWithDetails action: {ex.Message}");
        //        return StatusCode(500, "Internal server error");
        //    }
        //}

        /// <summary>
        /// Allow create a teacher
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Teacher), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public IActionResult CreateTeacher([FromBody]Teacher teacher)
        {
            try
            {
                if (teacher.IsObjectNull())
                {
                    _logger.LogError("Teacher object sent from client is null.");
                    return BadRequest("Teacher object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid teacher object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _teacherService.Create(teacher);

                return CreatedAtRoute("TeacherById", new { id = teacher.Id }, teacher);
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateTeacher action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Allow update a teacher
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Teacher), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public IActionResult UpdateTeacher(Guid id, [FromBody]Teacher teacher)
        {
            try
            {
                if (teacher.IsObjectNull())
                {
                    _logger.LogError("Teacher object sent from client is null.");
                    return BadRequest("Teacher object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid teacher object sent from client.");
                    return BadRequest("Invalid model object");
                }
                
                _teacherService.Update(teacher);

                return NoContent();
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateTeacher action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Allow delete a teacher
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Teacher), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public IActionResult DeleteTeacher(Guid TeacherId)
        {
            try
            {
                if (TeacherId == null || TeacherId == Guid.Empty)
                {
                    _logger.LogError("Teacher id null or empty");
                    return BadRequest("Teacher id is null or empty");
                }

                var dbTeacher = _teacherService.GetById(TeacherId);
                if (dbTeacher.IsEmptyObject())
                {
                    _logger.LogError($"Teacher with id: {TeacherId}, hasn't been found in db.");
                    return NotFound();
                }

                _teacherService.Delete(dbTeacher);
                return NoContent();
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteTeacher action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
