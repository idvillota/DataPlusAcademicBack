using DataPlus.Entities.Models;

namespace DataPlus.Entities.Extensions
{
    public static class StudentExtensions
    {
        public static void Map(this Student dbStudent, Student student)
        {
            dbStudent.FirstName = student.FirstName;
            dbStudent.LastName = student.LastName;
            dbStudent.Email = student.Email;
            dbStudent.Address = student.Address;
            dbStudent.DocumentNumber = student.DocumentNumber;
            dbStudent.DocumentType = student.DocumentType;
            dbStudent.PhoneNumber = student.PhoneNumber;
        }
    }
}
