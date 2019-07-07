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

namespace DataPlus.Test.Course
{
    public class CourseControllerTest : BaseControllerTest, IDisposable
    {
        private ICourseService _curseService;

        public CourseControllerTest()
        {
            _curseService = new CourseService(_repositoryWrapper, _logger);
        }

        [Fact]
        public void GetById_ExistingId_OkResult()
        {
            var controller = new CourseController(_logger, _curseService);
            var curseId = new Guid("1340dc3f-7818-4d9a-b7b3-76de28920134");
            var data = controller.GetCourseById(curseId);
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void GetById_NoExistingId_NotFoundResult()
        {
            var controller = new CourseController(_logger, _curseService);
            var curseId = new Guid("1340dc3f-7818-4d9a-b7b3-76de28912345");
            var data = controller.GetCourseById(curseId);
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public void GetById_NullId_BadRequestResult()
        {
            var controller = new CourseController(_logger, _curseService);
            Guid curseId = Guid.Empty;
            var data = controller.GetCourseById(curseId);
        }

        [Fact]
        public void GetAll_RequestCourses_OkResult()
        {
            var controller = new CourseController(_logger, _curseService);
            var data = controller.GetAllCourses();
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void GetAll_RequestCourses_MatchResult()
        {
            var controller = new CourseController(_logger, _curseService);
            var data = controller.GetAllCourses();
            Assert.IsType<OkObjectResult>(data);

            var curses = data as OkObjectResult;
            var curseList = curses.Value as IList<Entities.Models.Course>;

            Assert.Equal("4cba9635-3596-4197-9f70-1ffc6c9c9475", curseList[0].Id.ToString());
            Assert.Equal("Course 3", curseList[0].Name);
        }

        [Fact]
        public void Create_CourseWithValidData_OkResult()
        {
            var controller = new CourseController(_logger, _curseService);
            var curse = new Entities.Models.Course
            {
                Id = new Guid("8af705ea-513e-4c67-8a60-5454df4c1fa5"),
                Name = "Dummy Course",
                Description = "Dumm Course descrpition"
            };

            var data = controller.CreateCourse(curse);

            Assert.IsType<CreatedAtRouteResult>(data as CreatedAtRouteResult);
        }

        [Fact]
        public void Create_CourseWithInvalidData_BadRequest()
        {
            //var controller = new CourseController(_logger, _curseService);
            //var curse = new Entities.Models.Course
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

            //var data = controller.CreateCourse(curse);
            //Assert.IsType<BadRequestObjectResult>(data);
        }

        [Fact]
        public void Update_CourseWithValidData_OkResult()
        {
            var controller = new CourseController(_logger, _curseService);
            var curseId = new Guid("9031f9e4-b764-4444-827b-c3de35f28167");
            var existingCourse = controller.GetCourseById(curseId);

            Assert.IsType<OkObjectResult>(existingCourse);


            var curse = (existingCourse as ObjectResult).Value as Entities.Models.Course;
            curse.Name = "Course 2 Modified";

            var updatedData = controller.UpdateCourse(curseId, curse);

            Assert.IsType<NoContentResult>(updatedData);
        }

        [Fact]
        public void Delete_ExistingCourse_OkResult()
        {
            var controller = new CourseController(_logger, _curseService);
            var curse = new Entities.Models.Course
            {
                Id = new Guid("67e29680-a4e1-4eef-a762-eef2fb9702e3"),
                Name = "Dummy Course",
                Description = "Dumm Course descrpition"
            };

            var data = controller.CreateCourse(curse);
            Assert.IsType<CreatedAtRouteResult>(data as CreatedAtRouteResult);

            var deleteResult = controller.DeleteCourse(curse.Id);
            Assert.IsType<NoContentResult>(deleteResult);
        }

        [Fact]
        public void Delete_NonExistingCourse_NotFoundResult()
        {
            var controller = new CourseController(_logger, _curseService);
            var curseId = new Guid("67e29680-a4e1-4eef-a762-eef2fb912345");
            var data = controller.DeleteCourse(curseId);
            Assert.IsType<NotFoundResult>(data);
        }

        public void Dispose()
        {
            //Delete created curse:
            var createdCourse = _curseService.GetById(new Guid("8af705ea-513e-4c67-8a60-5454df4c1fa5"));
            if (createdCourse != null && createdCourse.Id != Guid.Empty)
                _curseService.Delete(createdCourse);

            //Recovery modified curse:
            var modifiedCourse = _curseService.GetById(new Guid("9031f9e4-b764-4444-827b-c3de35f28167"));
            if (modifiedCourse != null && modifiedCourse.Id != Guid.Empty)
            {
                modifiedCourse.Name = "Course 2";
                _curseService.Update(modifiedCourse);
            }

        }
    }
}
