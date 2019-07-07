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
    public class StudentController : ControllerBase
    {
        private ILoggerManager _logger;
        private IStudentService _studentService;

        public StudentController(ILoggerManager logger, IStudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;
        }
        
        // GET: api/Student
        [HttpGet]
        [ProducesResponseType(typeof(IList<Student>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public IActionResult GetAllStudents()
        {
            try
            {
                var students = _studentService.GetAll();
                _logger.LogInfo($"Returned all students from database.");
                return Ok(students);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllStudents action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get students filter by Id
        /// </summary>
        /// <param name="id">Student id</param>
        [HttpGet("{id}", Name = "StudentById")]
        [ProducesResponseType(typeof(Student), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public IActionResult GetStudentById(Guid id)
        {
            try
            {
                var student = _studentService.GetById(id);
                if (student.IsEmptyObject())
                {
                    _logger.LogError($"Student with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned student with id: {id}");
                    return Ok(student);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetStudentById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //[HttpGet("{id}/account")]
        //public IActionResult GetStudentWithDetails(Guid id)
        //{
        //    try
        //    {
        //        var student = _studentService.GetStudentWithDetails(id);

        //        if (student.Id.Equals(Guid.Empty))
        //        {
        //            _logger.LogError($"Student with id: {id}, hasn't been found in db.");
        //            return NotFound();
        //        }
        //        else
        //        {
        //            _logger.LogInfo($"Returned student with details for id: {id}");
        //            return Ok(student);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Something went wrong inside GetStudentWithDetails action: {ex.Message}");
        //        return StatusCode(500, "Internal server error");
        //    }
        //}

        /// <summary>
        /// Allow create a student
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Student), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public IActionResult CreateStudent([FromBody]Student student)
        {
            try
            {
                if (student.IsObjectNull())
                {
                    _logger.LogError("Student object sent from client is null.");
                    return BadRequest("Student object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid student object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _studentService.Create(student);
                return CreatedAtRoute("StudentById", new { id = student.Id }, student);
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateStudent action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Allow update a student
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Student), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public IActionResult UpdateStudent(Guid id, [FromBody]Student student)
        {
            try
            {
                if (student.IsObjectNull())
                {
                    _logger.LogError("Student object sent from client is null.");
                    return BadRequest("Student object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid student object sent from client.");
                    return BadRequest("Invalid model object");
                }
                
                _studentService.Update(student);

                return NoContent();
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateStudent action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Allow delete a student
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Student), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public IActionResult DeleteStudent(Guid StudentId)
        {
            try
            {
                if (StudentId == null || StudentId == Guid.Empty )                
                {
                    _logger.LogError("Student id null or empty");
                    return BadRequest("Student id is null or empty");
                }

                var dbStudent = _studentService.GetById(StudentId);
                if (dbStudent.IsEmptyObject())
                {
                    _logger.LogError($"Student with id: {StudentId}, hasn't been found in db.");
                    return NotFound();
                }

                _studentService.Delete(dbStudent);

                return NoContent();
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteStudent action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
