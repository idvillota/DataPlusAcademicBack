using DataPlus.Entities.Models;

namespace DataPlus.Entities.Extensions
{
    public static class TeacherExtensions
    {
        public static void Map(this Teacher dbTeacher, Teacher teacher)
        {
            dbTeacher.FirstName = teacher.FirstName;
            dbTeacher.LastName = teacher.LastName;
            dbTeacher.Email = teacher.Email;
            dbTeacher.Address = teacher.Address;
            dbTeacher.DocumentNumber = teacher.DocumentNumber;
            dbTeacher.DocumentType = teacher.DocumentType;
            dbTeacher.PhoneNumber = teacher.PhoneNumber;
        }
    }
}
