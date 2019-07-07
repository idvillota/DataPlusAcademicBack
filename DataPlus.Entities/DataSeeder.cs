using DataPlus.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPlus.Entities
{
    public class DataSeeder
    {
        private RepositoryContext _context;

        public DataSeeder(RepositoryContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            CreateSignatures();
            CreateCourses();
            CreateStudents();
            CreateTeachers();
            _context.SaveChanges();
        }

        private void CreateSignatures()
        {
            AddNewSignature(new Signature
            {
                Id = new Guid("EED7F976-58B7-4C05-E3E4-08D6F9F0FC2C"),
                Name = "Signature 1",
                Description = "This is the signature 1 description"
            });
            AddNewSignature(new Signature
            {
                Id = new Guid("5D01AAEE-4E4B-416C-E3E5-08D6F9F0FC2C"),
                Name = "Signature 2",
                Description = "This is the signature 2 description"
            });
            AddNewSignature(new Signature
            {
                Id = new Guid("F3AEC6A8-DEEA-4F61-E3E6-08D6F9F0FC2C"),
                Name = "Signature 3",
                Description = "This is the signature 3 description"
            });
            AddNewSignature(new Signature
            {
                Id = new Guid("BAB39736-294C-487D-E3E7-08D6F9F0FC2C"),
                Name = "Signature 4",
                Description = "This is the signature 4 description"
            });
            AddNewSignature(new Signature
            {
                Id = new Guid("3309025A-E4A4-4B45-E3E8-08D6F9F0FC2C"),
                Name = "Signature 5",
                Description = "This is the signature 5 description"
            });
        }

        private void CreateCourses()
        {
            AddNewCourse(new Course
            {
                Id = new Guid("4cba9635-3596-4197-9f70-1ffc6c9c9475"),
                Name = "Course 3",
                Description = "Course 3 is the third course"
            });
            AddNewCourse(new Course
            {
                Id = new Guid("1340dc3f-7818-4d9a-b7b3-76de28920134"),
                Name = "Course 1",
                Description = "Course 1 is the first course"                
            });
            AddNewCourse(new Course
            {
                Id = new Guid("9031f9e4-b764-4444-827b-c3de35f28167"),
                Name = "Course 2",
                Description = "Course 2 is the second course"
            });
        }

        private void CreateStudents()
        {
            AddNewStudent(new Student
            {
                Id = new Guid("f4c505a9-f755-4176-ab01-5f0248f0d143"),
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
            AddNewStudent(new Student
            {
                Id = new Guid("0673e10f-7e88-4fe6-a968-2f519af7b6b6"),
                DocumentNumber = "333333",
                FirstName = "Maria",
                LastName = "Diaz",
                Email = "maria@gmail.com",
                Address = "Street 3",
                City = "City 3",
                DocumentType = EDocumentType.CC,
                PhoneNumber = "7111111",
                Birth = new DateTime(1970, 1, 1)
            });
            AddNewStudent(new Student
            {
                Id = new Guid("25c34068-cd62-44b9-b79a-42449dbb1850"),
                DocumentNumber = "555555",
                FirstName = "Gabriel",
                LastName = "Villota",
                Email = "gabriel@gmail.com",
                Address = "Street 4",
                City = "City 2",
                DocumentType = EDocumentType.TI,
                PhoneNumber = "7111111",
                Birth = new DateTime(1960, 1, 1)
            });
            AddNewStudent(new Student
            {
                Id = new Guid("a67e2513-1adb-4af5-a282-9022d2b01a6a"),
                DocumentNumber = "444444",
                FirstName = "Ivan",
                LastName = "Villota",
                Email = "ivane@gmail.com",
                Address = "Street 2",
                City = "City 3",
                DocumentType = EDocumentType.CC,
                PhoneNumber = "7111111",
                Birth = new DateTime(1990, 1, 1)
            });
            AddNewStudent(new Student
            {
                Id = new Guid("6ba7a26a-200d-41f9-883d-2655db92e060"),
                DocumentNumber = "222222",
                FirstName = "Sasha",
                LastName = "Santos",
                Email = "sasha@gmail.com",
                Address = "Street 1",
                City = "City 1",
                DocumentType = EDocumentType.CC,
                PhoneNumber = "7111111",
                Birth = new DateTime(1980, 1, 1)
            });
        }

        private void CreateTeachers()
        {
            AddNewTeacher(new Teacher { Id = new Guid("259663d0-9932-497e-b0dd-59f5bb61b558"), DocumentNumber = "10101010", FirstName = "LIliana",
                LastName = "Villota", Email = "pepe@gmail.com", Address = "Street 1", City = "City 1",
                DocumentType = EDocumentType.TI, PhoneNumber = "7111111", Birth = new DateTime(1980, 1, 1) });
            AddNewTeacher(new Teacher { Id = new Guid("d3d78005-96ac-463a-a740-6fff9a871e12"), DocumentNumber = "99999999", FirstName = "Myriam",
                LastName = "Burbano", Email = "maria@gmail.com", Address = "Street 3", City = "City 3",
                DocumentType = EDocumentType.CC, PhoneNumber = "7111111", Birth = new DateTime(1970, 1, 1) });
            AddNewTeacher(new Teacher { Id = new Guid("607625b1-237f-4bc5-93ef-36c683eef465"), DocumentNumber = "66666666", FirstName = "Juan David",
                LastName = "Narvez", Email = "gabriel@gmail.com", Address = "Street 4", City = "City 2",
                DocumentType = EDocumentType.TI, PhoneNumber = "7111111", Birth = new DateTime(1960, 1, 1) });
            AddNewTeacher(new Teacher { Id = new Guid("2522452f-eadb-4fde-969d-3b95f5fee55c"), DocumentNumber = "7777777", FirstName = "Ruben Dario",
                LastName = "Lopez", Email = "ivane@gmail.com", Address = "Street 2", City = "City 3",
                DocumentType = EDocumentType.CC, PhoneNumber = "7111111", Birth = new DateTime(1990, 1, 1) });
            AddNewTeacher(new Teacher { Id = new Guid("5a9b2b9c-2ed3-44c5-a312-d1d148936cb9"), DocumentNumber = "8888888", FirstName = "Ernesto",
                LastName = "Villota", Email = "sasha@gmail.com", Address = "Street 1", City = "City 1",
                DocumentType = EDocumentType.CC, PhoneNumber = "7111111", Birth = new DateTime(1980, 1, 1) });
        }
        
        private void AddNewSignature(Signature signature)
        {
            var existingSignature = _context.Signatures.FirstOrDefault(s => s.Name.Equals(signature.Name));
            if (existingSignature == null)
                _context.Signatures.Add(signature);
        }

        private void AddNewCourse(Course course)
        {
            var existingCourse = _context.Courses.FirstOrDefault(c => c.Name.Equals(course.Name));
            if (existingCourse == null)
                _context.Courses.Add(course);
        }

        private void AddNewTeacher(Teacher teacher)
        {
            var existingTeacher = _context.Teachers.FirstOrDefault(t => t.DocumentNumber.Equals(teacher.DocumentNumber));
            if (existingTeacher == null)
                _context.Teachers.Add(teacher);
        }

        private void AddNewStudent(Student student)
        {
            var existingStudent = _context.Students.FirstOrDefault(s => s.DocumentNumber.Equals(student.DocumentNumber));
            if (existingStudent == null)
                _context.Students.Add(student);
        }

    }
}
