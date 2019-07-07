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

namespace DataPlus.Test.Teacher
{
    public class TeacherControllerTest : BaseControllerTest, IDisposable
    {
        private ITeacherService _teacherService;
        
        public TeacherControllerTest()
        {
            _teacherService = new TeacherService(_repositoryWrapper, _logger);
        }

        [Fact]
        public void GetById_ExistingId_OkResult()
        {
            var controller = new TeacherController(_logger, _teacherService);
            var teacherId = new Guid("259663d0-9932-497e-b0dd-59f5bb61b558");
            var data = controller.GetTeacherById(teacherId);
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void GetById_NoExistingId_NotFoundResult()
        {
            var controller = new TeacherController(_logger, _teacherService);
            var teacherId = new Guid("259663d0-9932-497e-b0dd-59f5bb612345");
            var data = controller.GetTeacherById(teacherId);
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public void GetById_NullId_BadRequestResult()
        {
            var controller = new TeacherController(_logger, _teacherService);
            Guid teacherId = Guid.Empty;
            var data = controller.GetTeacherById(teacherId);
        }

        [Fact]
        public void GetAll_RequestTeachers_OkResult()
        {
            var controller = new TeacherController(_logger, _teacherService);
            var data = controller.GetAllTeachers();
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void GetAll_RequestTeachers_MatchResult()
        {
            var controller = new TeacherController(_logger, _teacherService);
            var data = controller.GetAllTeachers();
            Assert.IsType<OkObjectResult>(data);

            var teachers = data as OkObjectResult;
            var teacherList = teachers.Value as IList<Entities.Models.Teacher>;

            Assert.Equal("607625b1-237f-4bc5-93ef-36c683eef465", teacherList[0].Id.ToString());
            Assert.Equal("Juan David", teacherList[0].FirstName);            
        }

        [Fact]
        public void Create_TeacherWithValidData_OkResult()
        {
            var controller = new TeacherController(_logger, _teacherService);
            var teacher = new Entities.Models.Teacher
            {
                Id = new Guid("a6a79ee2-3e9d-4abd-86ce-6acb1958e76a"),
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

            var data = controller.CreateTeacher(teacher);

            Assert.IsType<CreatedAtRouteResult>(data as CreatedAtRouteResult);            
        }

        [Fact]
        public void Create_TeacherWithInvalidData_BadRequest()
        {
            //var controller = new TeacherController(_logger, _teacherService);
            //var teacher = new Entities.Models.Teacher
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

            //var data = controller.CreateTeacher(teacher);
            //Assert.IsType<BadRequestObjectResult>(data);
        }

        [Fact]
        public void Update_TeacherWithValidData_OkResult()
        {
            var controller = new TeacherController(_logger, _teacherService);
            var teacherId = new Guid("259663D0-9932-497E-B0DD-59F5BB61B558");
            var existingTeacher = controller.GetTeacherById(teacherId);

            Assert.IsType<OkObjectResult>(existingTeacher);


            var teacher = (existingTeacher as ObjectResult).Value as Entities.Models.Teacher;
            teacher.FirstName = "Name Modified";

            var updatedData = controller.UpdateTeacher(teacherId, teacher);

            Assert.IsType<NoContentResult>(updatedData);
        }

        [Fact]
        public void Delete_ExistingTeacher_OkResult()
        {
            var controller = new TeacherController(_logger, _teacherService);
            var teacher = new Entities.Models.Teacher
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

            var data = controller.CreateTeacher(teacher);
            Assert.IsType<CreatedAtRouteResult>(data as CreatedAtRouteResult);

            var deleteResult = controller.DeleteTeacher(teacher.Id);
            Assert.IsType<NoContentResult>(deleteResult);
        }

        [Fact]
        public void Delete_NonExistingTeacher_NotFoundResult()
        {
            var controller = new TeacherController(_logger, _teacherService);
            var teacherId = new Guid("1c575a69-27f3-4379-b7be-5798a0001449");
            var data = controller.DeleteTeacher(teacherId);
            Assert.IsType<NotFoundResult>(data);
        }

        public void Dispose()
        {
            //Delete created teacher:
            var createdTeacher = _teacherService.GetById(new Guid("a6a79ee2-3e9d-4abd-86ce-6acb1958e76a"));
            if (createdTeacher != null && createdTeacher.Id != Guid.Empty)
                _teacherService.Delete(createdTeacher);

            //Recovery modified teacher:
            var modifiedTeacher = _teacherService.GetById(new Guid("259663D0-9932-497E-B0DD-59F5BB61B558"));
            if (modifiedTeacher != null && modifiedTeacher.Id != Guid.Empty)
            {
                modifiedTeacher.FirstName = "LIliana";
                _teacherService.Update(modifiedTeacher);
            }
            
        }
    }
}
