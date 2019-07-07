using DataPlus.Contracts.Repositories;
using DataPlus.Entities;
using DataPlus.Entities.Extensions;
using DataPlus.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataPlus.Repository
{
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        //public IEnumerable<Student> GetAllStudents()
        //{
        //    return FindAll()
        //        .OrderBy(s => s.FirstName)
        //        .ToList();
        //}

        //public Student GetStudentById(Guid ownerId)
        //{
        //    return FindByCondition(owner => owner.Id.Equals(ownerId))
        //        .DefaultIfEmpty(new Student())
        //        .FirstOrDefault();
        //}

        //public StudentExtended GetStudentWithDetails(Guid studentId)
        //{
        //    return new StudentExtended(GetStudentById(studentId));
        //    //{
        //    //    Accounts = RepositoryContext.Accounts
        //    //        .Where(a => a.OwnerId == ownerId)
        //    //};
        //}

        //public void Create(Student student)
        //{
        //    student.Id = Guid.NewGuid();
        //    Create(student);
        //}

        //public void Update(Student dbStudent, Student student)
        //{
        //    dbStudent.Map(student);
        //    Update(dbStudent);
        //}

        //public void Delete(Student student)
        //{
        //    Delete(student);
        //}

    }
}
