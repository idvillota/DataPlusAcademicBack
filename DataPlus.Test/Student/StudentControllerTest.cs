using DataPlus.Academic.Controllers.Api;
using DataPlus.Contracts.Logger;
using DataPlus.Contracts.Repositories;
using DataPlus.Contracts.Services;
using DataPlus.Entities;
using DataPlus.Entities.Models;
using DataPlus.Repository;
using DataPlus.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DataPlus.Test.Student
{
    public class TeacherControllerTest : BaseControllerTest, IDisposable
    {
        private IStudentService _studentService;
        
        public TeacherControllerTest()
        {
            _studentService = new StudentService(_repositoryWrapper, _logger);
        }

        [Fact]
        public void GetById_ExistingId_OkResult()
        {
            var controller = new StudentController(_logger, _studentService);
            var studentId = new Guid("6BA7A26A-200D-41F9-883D-2655DB92E060");
            var data = controller.GetStudentById(studentId);
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void GetById_NoExistingId_NotFoundResult()
        {
            var controller = new StudentController(_logger, _studentService);
            var studentId = new Guid("A9253E48-A1AB-4F83-A1A7-0E14D0F12345");
            var data = controller.GetStudentById(studentId);
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public void GetById_NullId_BadRequestResult()
        {
            var controller = new StudentController(_logger, _studentService);
            Guid studentId = Guid.Empty;
            var data = controller.GetStudentById(studentId);
        }

        [Fact]
        public void GetAll_RequestStudents_OkResult()
        {
            var controller = new StudentController(_logger, _studentService);
            var data = controller.GetAllStudents();
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void GetAll_RequestStudents_MatchResult()
        {
            var controller = new StudentController(_logger, _studentService);
            var data = controller.GetAllStudents();
            Assert.IsType<OkObjectResult>(data);

            var students = data as OkObjectResult;
            var studentList = students.Value as IList<Entities.Models.Student>;

            Assert.Equal("6ba7a26a-200d-41f9-883d-2655db92e060", studentList[0].Id.ToString());
            Assert.Equal("Sasha", studentList[0].FirstName);            
        }

        [Fact]
        public void Create_StudentWithValidData_OkResult()
        {
            var controller = new StudentController(_logger, _studentService);
            var student = new Entities.Models.Student
            {
                Id = new Guid("e47c0cb5-05b6-437d-b40f-f2c5b5a08385"),
                FirstName = "Homero",
                LastName = "Simpson",
                DocumentType = Entities.Models.EDocumentType.CC,
                DocumentNumber = "1245687",
                Address = "Springfield 123",
                Email = "homero@hotmail.com",
                City = "Springfield",
                PhoneNumber = "7654321",
                Birth = new DateTime(1950, 11, 1)
            };

            var data = controller.CreateStudent(student);

            Assert.IsType<CreatedAtRouteResult>(data as CreatedAtRouteResult);            
        }

        [Fact]
        public void Create_StudentWithInvalidData_BadRequest()
        {
            //var controller = new StudentController(_logger, _studentService);
            //var student = new Entities.Models.Student
            //{
            //    //Id = new Guid("e47c0cb5-05b6-437d-b40f-f2c5b5a08385"),
            //    FirstName = "HomeroHomeroHomeroHomeroHomeroHomeroHomeroHomeroHomeroHomeroHomeroHomeroHomeroHomero",
            //    LastName = "Simpson",
            //    DocumentType = Entities.Models.EDocumentType.CC,
            //    DocumentNumber = "1245687",
            //    Address = "Springfield 123",
            //    Email = "homero@hotmail.com",
            //    City = "Springfield",
            //    PhoneNumber = "7654321",
            //    Birth = new DateTime(1950, 11, 1)
            //};

            //var data = controller.CreateStudent(student);
            //Assert.IsType<BadRequestObjectResult>(data);
        }

        [Fact]
        public void Update_StudentWithValidData_OkResult()
        {
            var controller = new StudentController(_logger, _studentService);
            var studentId = new Guid("0673E10F-7E88-4FE6-A968-2F519AF7B6B6");
            var existingStudent = controller.GetStudentById(studentId);

            Assert.IsType<OkObjectResult>(existingStudent);


            var student = (existingStudent as ObjectResult).Value as Entities.Models.Student;
            student.FirstName = "Maria Modified";

            var updatedData = controller.UpdateStudent(studentId, student);

            Assert.IsType<NoContentResult>(updatedData);
        }

        [Fact]
        public void Delete_ExistingStudent_OkResult()
        {
            var controller = new StudentController(_logger, _studentService);
            var student = new Entities.Models.Student
            {
                Id = new Guid("f7afefa9-2cc6-4ea9-901d-d99e227a12de"),
                FirstName = "Bart",
                LastName = "Simpson",
                DocumentType = Entities.Models.EDocumentType.CC,
                DocumentNumber = "97654315",
                Address = "Springfield 123",
                Email = "bart@hotmail.com",
                City = "Springfield",
                PhoneNumber = "7654321",
                Birth = new DateTime(1950, 11, 1)
            };

            var data = controller.CreateStudent(student);
            Assert.IsType<CreatedAtRouteResult>(data as CreatedAtRouteResult);

            var deleteResult = controller.DeleteStudent(student.Id);
            Assert.IsType<NoContentResult>(deleteResult);
        }

        [Fact]
        public void Delete_NonExistingStudent_NotFoundResult()
        {
            var controller = new StudentController(_logger, _studentService);
            var studentId = new Guid("1c575a69-27f3-4379-b7be-5798a0001449");
            var data = controller.DeleteStudent(studentId);
            Assert.IsType<NotFoundResult>(data);
        }

        public void Dispose()
        {
            //Delete created student:
            var createdStudent = _studentService.GetById(new Guid("e47c0cb5-05b6-437d-b40f-f2c5b5a08385"));
            if (createdStudent != null && createdStudent.Id != Guid.Empty)
                _studentService.Delete(createdStudent);

            //Recovery modified student:
            var modifiedStudent = _studentService.GetById(new Guid("0673E10F-7E88-4FE6-A968-2F519AF7B6B6"));
            if (modifiedStudent != null && modifiedStudent.Id != Guid.Empty)
            {
                modifiedStudent.FirstName = "Maria";
                _studentService.Update(modifiedStudent);
            }
            
        }
    }
}
