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

namespace DataPlus.Test.Teacher
{
    public class TeacherServiceTest
    {
        private IList<DataPlus.Entities.Models.Teacher> _teacherList;

        private ILoggerManager _logger;

        public TeacherServiceTest()
        {
            _teacherList = GetTeacherList();
            _logger = new Mock<ILoggerManager>().Object;
        }

        private IList<DataPlus.Entities.Models.Teacher> GetTeacherList()
        {
            return new List<DataPlus.Entities.Models.Teacher>
            {
                new DataPlus.Entities.Models.Teacher
            {
                Id = Guid.NewGuid(),
                DocumentNumber = "111111",
                FirstName = "Pepe",
                LastName = "Perez",
                Email = "pepe@gmail.com",
                Address = "Street 1",
                City = "City 1",
                DocumentType = EDocumentType.TI,
                PhoneNumber = "7111111",
                Birth = new DateTime(1980, 1, 1)
            }, new DataPlus.Entities.Models.Teacher
            {
                Id = Guid.NewGuid(),
                DocumentNumber = "333333",
                FirstName = "Maria",
                LastName = "Diaz",
                Email = "maria@gmail.com",
                Address = "Street 3",
                City = "City 3",
                DocumentType = EDocumentType.CC,
                PhoneNumber = "7111111",
                Birth = new DateTime(1970, 1, 1)
            },new DataPlus.Entities.Models.Teacher
            {
                Id = Guid.NewGuid(),
                DocumentNumber = "555555",
                FirstName = "Gabriel",
                LastName = "Villota",
                Email = "gabriel@gmail.com",
                Address = "Street 4",
                City = "City 2",
                DocumentType = EDocumentType.TI,
                PhoneNumber = "7111111",
                Birth = new DateTime(1960, 1, 1)
            },
                new DataPlus.Entities.Models.Teacher
            {
                Id = Guid.NewGuid(),
                DocumentNumber = "444444",
                FirstName = "Ivan",
                LastName = "Villota",
                Email = "ivane@gmail.com",
                Address = "Street 2",
                City = "City 3",
                DocumentType = EDocumentType.CC,
                PhoneNumber = "7111111",
                Birth = new DateTime(1990, 1, 1)
            }
            };

        }

        [Fact]
        public void GetAll_RequestALlistOfTeachers_ListOfTeachers()
        {
            var repositoryWrapper = new Mock<IWrapperRepository>();
            repositoryWrapper.Setup(x => x.Teacher.GetAll()).Returns(_teacherList.AsQueryable);

            var teacherService = new TeacherService(repositoryWrapper.Object, _logger);
            var teachers = teacherService.GetAll();

            Assert.True(teachers != null);
            Assert.True(teachers.Count == 4);
        }

        [Fact]
        public void GetAll_RequestAListOfTeachersWithNullRepositoryWrapper_Exception()
        {
            var teacherService = new TeacherService(null, _logger);
            Assert.Throws<NullReferenceException>(() => teacherService.GetAll());
        }

        [Fact]
        public void GetById_DummyTeacherId_EmptyTeacher()
        {
            var repositoryWrapper = new Mock<IWrapperRepository>();
            var teacherId = new Guid("8dedfd1f-45b8-4a8f-a25f-730e352ca629");
            repositoryWrapper.Setup(x => x.Teacher.GetById(teacherId)).Returns(new Entities.Models.Teacher());

            var teacherService = new TeacherService(repositoryWrapper.Object, _logger);
            var teacher = teacherService.GetById(teacherId);

            Assert.True(teacher.FirstName == null);
            Assert.True(teacher.Id == Guid.Empty);
        }

        [Fact]
        public void GetById_SendAndIdToGetAnTeacher_TeacherFilteredById()
        {
            var repositoryWrapper = new Mock<IWrapperRepository>();
            var teacherId = _teacherList.First().Id;
            repositoryWrapper.Setup(x => x.Teacher.GetById(teacherId)).Returns(new DataPlus.Entities.Models.Teacher
            {
                Id = teacherId,
                DocumentNumber = "111111",
                FirstName = "Pepe",
                LastName = "Perez",
                Email = "pepe@gmail.com",
                Address = "Street 1",
                City = "City 1",
                DocumentType = EDocumentType.TI,
                PhoneNumber = "7111111",
                Birth = new DateTime(1980, 1, 1)
            });

            var teacherService = new TeacherService(repositoryWrapper.Object, _logger);
            var teacher = teacherService.GetById(teacherId);

            Assert.True(teacher != null);
            Assert.True(teacher.FirstName == "Pepe");
            Assert.True(teacher.LastName == "Perez");
        }

        [Fact]
        public void Update_TeacherWithAllData_TeacherUpdated()
        {
            var teacherId = _teacherList.First().Id;
            var repositoryWrapper = new Mock<IWrapperRepository>();
            repositoryWrapper.Setup(x => x.Teacher.GetAll()).Returns(_teacherList.AsQueryable);
            repositoryWrapper.Setup(x => x.Teacher.GetById(teacherId)).Returns(_teacherList.First(s => s.Id == teacherId));

            TeacherService teacherService = new TeacherService(repositoryWrapper.Object, _logger);

            var teacher = teacherService.GetById(teacherId);
            teacher.FirstName = teacher.FirstName + "_modified";

            teacherService.Update(teacher);

            var updatedTeacher = teacherService.GetById(teacher.Id);
            Assert.Contains("_modified", updatedTeacher.FirstName);
        }

        [Fact]
        public void Update_TeacherWithoutId_Exception()
        {
            var teacher = new DataPlus.Entities.Models.Teacher();
            var repositoryWrapper = new Mock<IWrapperRepository>();
            var teacherService = new TeacherService(repositoryWrapper.Object, _logger);

            Assert.Throws<NullReferenceException>(() => teacherService.Update(teacher));
        }

        [Fact]
        public void Create_TeacherWithData_NewTeacher()
        {
            var newTeacher = new DataPlus.Entities.Models.Teacher
            {
                Id = Guid.NewGuid(),
                DocumentNumber = "5555555",
                FirstName = "Jose",
                LastName = "Santos",
                Email = "jose@hotmail.com",
                Address = "Street 5",
                City = "City 2",
                DocumentType = EDocumentType.CC,
                PhoneNumber = "722222",
                Birth = new DateTime(1965, 5, 5)
            };

            var repositoryWrapper = new Mock<IWrapperRepository>();
            repositoryWrapper.Setup(x => x.Teacher.GetAll()).Returns(_teacherList.AsQueryable);

            var teacherService = new TeacherService(repositoryWrapper.Object, _logger);
            var numberOfTeachers = teacherService.GetAll().Count;
            teacherService.Create(newTeacher);

        }

        [Fact]
        public void Create_EmptyTeacher_Exception()
        {
            var newTeacher = new DataPlus.Entities.Models.Teacher();
            var repositoryWrapper = new Mock<IWrapperRepository>();
            var teacherService = new TeacherService(repositoryWrapper.Object, _logger);

            Assert.Throws<NullReferenceException>(() => teacherService.Create(newTeacher));
        }

        [Fact]
        public void Delete_ExistingTeacher_Successfully()
        {
            var teacherToDelete = new Entities.Models.Teacher();
            var repositoryWrapper = new Mock<IWrapperRepository>();
            repositoryWrapper.Setup(x => x.Teacher.Delete(teacherToDelete));

            var teacherService = new TeacherService(repositoryWrapper.Object, _logger);
            teacherService.Delete(teacherToDelete);

        }
    }
}
