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

namespace DataPlus.Test.Student
{
    public class TeacherServiceTest
    {
        private IList<DataPlus.Entities.Models.Student> _studentList;

        private ILoggerManager _logger;

        public TeacherServiceTest()
        {
            _studentList = GetStudentList();
            _logger = new Mock<ILoggerManager>().Object;
        }

        private IList<DataPlus.Entities.Models.Student> GetStudentList()
        {
            return new List<DataPlus.Entities.Models.Student>
            {
                new DataPlus.Entities.Models.Student
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
            }, new DataPlus.Entities.Models.Student
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
            },new DataPlus.Entities.Models.Student
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
                new DataPlus.Entities.Models.Student
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
        public void GetAll_RequestALlistOfStudents_ListOfStudents()
        {
            var repositoryWrapper = new Mock<IWrapperRepository>();
            repositoryWrapper.Setup(x => x.Student.GetAll()).Returns(_studentList.AsQueryable);

            var studentService = new StudentService(repositoryWrapper.Object, _logger);
            var students = studentService.GetAll();

            Assert.True(students != null);
            Assert.True(students.Count == 4);
        }

        [Fact]
        public void GetAll_RequestAListOfStudentsWithNullRepositoryWrapper_Exception()
        {
            var studentService = new StudentService(null, _logger);
            Assert.Throws<NullReferenceException>(() => studentService.GetAll());
        }

        [Fact]
        public void GetById_DummyStudentId_EmptyStudent()
        {
            var repositoryWrapper = new Mock<IWrapperRepository>();
            var studentId = new Guid("8dedfd1f-45b8-4a8f-a25f-730e352ca629");
            repositoryWrapper.Setup(x => x.Student.GetById(studentId)).Returns(new Entities.Models.Student());

            var studentService = new StudentService(repositoryWrapper.Object, _logger);
            var student = studentService.GetById(studentId);

            Assert.True(student.FirstName == null);
            Assert.True(student.Id == Guid.Empty);
        }

        [Fact]
        public void GetById_SendAndIdToGetAnStudent_StudentFilteredById()
        {
            var repositoryWrapper = new Mock<IWrapperRepository>();
            var studentId = _studentList.First().Id;
            repositoryWrapper.Setup(x => x.Student.GetById(studentId)).Returns(new DataPlus.Entities.Models.Student
            {
                Id = studentId,
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

            var studentService = new StudentService(repositoryWrapper.Object, _logger);
            var student = studentService.GetById(studentId);

            Assert.True(student != null);
            Assert.True(student.FirstName == "Pepe");
            Assert.True(student.LastName == "Perez");
        }

        [Fact]
        public void Update_StudentWithAllData_StudentUpdated()
        {
            var studentId = _studentList.First().Id;
            var repositoryWrapper = new Mock<IWrapperRepository>();
            repositoryWrapper.Setup(x => x.Student.GetAll()).Returns(_studentList.AsQueryable);
            repositoryWrapper.Setup(x => x.Student.GetById(studentId)).Returns(_studentList.First(s => s.Id == studentId));

            StudentService studentService = new StudentService(repositoryWrapper.Object, _logger);

            var student = studentService.GetById(studentId);
            student.FirstName = student.FirstName + "_modified";

            studentService.Update(student);

            var updatedStudent = studentService.GetById(student.Id);
            Assert.Contains("_modified", updatedStudent.FirstName);
        }

        [Fact]
        public void Update_StudentWithoutId_Exception()
        {
            var student = new DataPlus.Entities.Models.Student();
            var repositoryWrapper = new Mock<IWrapperRepository>();
            var studentService = new StudentService(repositoryWrapper.Object, _logger);

            Assert.Throws<NullReferenceException>(() => studentService.Update(student));
        }

        [Fact]
        public void Create_StudentWithData_NewStudent()
        {
            var newStudent = new DataPlus.Entities.Models.Student
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
            repositoryWrapper.Setup(x => x.Student.GetAll()).Returns(_studentList.AsQueryable);

            var studentService = new StudentService(repositoryWrapper.Object, _logger);
            var numberOfStudents = studentService.GetAll().Count;
            studentService.Create(newStudent);

        }

        [Fact]
        public void Create_EmptyStudent_Exception()
        {
            var newStudent = new DataPlus.Entities.Models.Student();
            var repositoryWrapper = new Mock<IWrapperRepository>();
            var studentService = new StudentService(repositoryWrapper.Object, _logger);

            Assert.Throws<NullReferenceException>(() => studentService.Create(newStudent));
        }

        [Fact]
        public void Delete_ExistingStudent_Successfully()
        {
            var studentToDelete = new Entities.Models.Student();
            var repositoryWrapper = new Mock<IWrapperRepository>();
            repositoryWrapper.Setup(x => x.Student.Delete(studentToDelete));

            var studentService = new StudentService(repositoryWrapper.Object, _logger);
            studentService.Delete(studentToDelete);

        }
    }
}
