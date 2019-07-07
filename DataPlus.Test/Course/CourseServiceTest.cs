using DataPlus.Contracts.Logger;
using DataPlus.Contracts.Repositories;
using DataPlus.Contracts.Services;
using DataPlus.Entities;
using DataPlus.Entities.Models;
using DataPlus.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;


namespace DataPlus.Test.Course
{
    public class CourseServiceTest
    {
        private IList<DataPlus.Entities.Models.Course> _courseList;

        private ILoggerManager _logger;

        public CourseServiceTest()
        {
            _courseList = GetCourseList();
            _logger = new Mock<ILoggerManager>().Object;
        }

        private IList<DataPlus.Entities.Models.Course> GetCourseList()
        {
            return new List<DataPlus.Entities.Models.Course>
            {
                new DataPlus.Entities.Models.Course
            {
                Id = new Guid("4CBA9635-3596-4197-9F70-1FFC6C9C9475"),
                Name = "Course 3",
                Description = "Course 3 is the third course"
            }, new DataPlus.Entities.Models.Course
            {
                Id = new Guid("1340DC3F-7818-4D9A-B7B3-76DE28920134"),
                Name = "Course 1",
                Description = "Course 1 is the first course"
            },new DataPlus.Entities.Models.Course
            {
                Id = new Guid("9031F9E4-B764-4444-827B-C3DE35F28167"),
                Name = "Course 2",
                Description = "Course 2 is the second course"
            }
            };
        }

        [Fact]
        public void GetAll_RequestALlistOfCourses_ListOfCourses()
        {
            var repositoryWrapper = new Mock<IWrapperRepository>();
            repositoryWrapper.Setup(x => x.Course.GetAll()).Returns(_courseList.AsQueryable);

            var courseService = new CourseService(repositoryWrapper.Object, _logger);
            var courses = courseService.GetAll();

            Assert.True(courses != null);
            Assert.True(courses.Count == 3);
        }

        [Fact]
        public void GetAll_RequestAListOfCoursesWithNullRepositoryWrapper_Exception()
        {
            var courseService = new CourseService(null, _logger);
            Assert.Throws<NullReferenceException>(() => courseService.GetAll());
        }

        [Fact]
        public void GetById_DummyCourseId_EmptyCourse()
        {
            var repositoryWrapper = new Mock<IWrapperRepository>();
            var courseId = new Guid("8dedfd1f-45b8-4a8f-a25f-730e352ca629");
            repositoryWrapper.Setup(x => x.Course.GetById(courseId)).Returns(new Entities.Models.Course());

            var courseService = new CourseService(repositoryWrapper.Object, _logger);
            var course = courseService.GetById(courseId);

            Assert.True(course.Name == null);
            Assert.True(course.Id == Guid.Empty);
        }

        [Fact]
        public void GetById_SendAndIdToGetAnCourse_CourseFilteredById()
        {
            var repositoryWrapper = new Mock<IWrapperRepository>();
            var courseId = _courseList.First().Id;
            repositoryWrapper.Setup(x => x.Course.GetById(courseId)).Returns(new DataPlus.Entities.Models.Course
            {
                Id = courseId,
                Name = "Course 3",
                Description = "Course 3 is the third course"
            });

            var courseService = new CourseService(repositoryWrapper.Object, _logger);
            var course = courseService.GetById(courseId);

            Assert.True(course != null);
            Assert.True(course.Name == "Course 3");
            Assert.True(course.Description == "Course 3 is the third course");
        }

        [Fact]
        public void Update_CourseWithAllData_CourseUpdated()
        {
            var courseId = _courseList.First().Id;
            var repositoryWrapper = new Mock<IWrapperRepository>();
            repositoryWrapper.Setup(x => x.Course.GetAll()).Returns(_courseList.AsQueryable);
            repositoryWrapper.Setup(x => x.Course.GetById(courseId)).Returns(_courseList.First(s => s.Id == courseId));

            CourseService courseService = new CourseService(repositoryWrapper.Object, _logger);

            var course = courseService.GetById(courseId);
            course.Name = course.Name + "_modified";

            courseService.Update(course);

            var updatedCourse = courseService.GetById(course.Id);
            Assert.Contains("_modified", updatedCourse.Name);
        }

        [Fact]
        public void Update_CourseWithoutId_Exception()
        {
            var course = new DataPlus.Entities.Models.Course();
            var repositoryWrapper = new Mock<IWrapperRepository>();
            var courseService = new CourseService(repositoryWrapper.Object, _logger);

            Assert.Throws<NullReferenceException>(() => courseService.Update(course));
        }

        [Fact]
        public void Create_CourseWithData_NewCourse()
        {
            var newCourse = new DataPlus.Entities.Models.Course
            {
                Id = new Guid("1340DC3F-7818-4D9A-B7B3-76DE28920134"),
                Name = "Course 6",
                Description = "Course 6 is the third course"
            };

            var repositoryWrapper = new Mock<IWrapperRepository>();
            repositoryWrapper.Setup(x => x.Course.GetAll()).Returns(_courseList.AsQueryable);

            var courseService = new CourseService(repositoryWrapper.Object, _logger);
            var numberOfCourses = courseService.GetAll().Count;
            courseService.Create(newCourse);

        }

        [Fact]
        public void Create_EmptyCourse_Exception()
        {
            var newCourse = new DataPlus.Entities.Models.Course();
            var repositoryWrapper = new Mock<IWrapperRepository>();
            var courseService = new CourseService(repositoryWrapper.Object, _logger);

            Assert.Throws<NullReferenceException>(() => courseService.Create(newCourse));
        }

        [Fact]
        public void Delete_ExistingCourse_Successfully()
        {
            var courseToDelete = new Entities.Models.Course();
            var repositoryWrapper = new Mock<IWrapperRepository>();
            repositoryWrapper.Setup(x => x.Course.Delete(courseToDelete));

            var courseService = new CourseService(repositoryWrapper.Object, _logger);
            courseService.Delete(courseToDelete);

        }
    }
}