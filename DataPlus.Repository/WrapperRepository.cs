using DataPlus.Contracts.Repositories;
using DataPlus.Entities;

namespace DataPlus.Repository
{
    public class WrapperRepository : IWrapperRepository
    {
        private RepositoryContext _repoContext;
        private IStudentRepository _student;
        private ITeacherRepository _teacher;
        private ICourseRepository _course;
        private ISignatureRepository _signature;

        public ICourseRepository Course
        {
            get
            {
                _course = _course == null ? new CourseRepository(_repoContext) : _course;
                return _course;
            }
        }

        public ISignatureRepository Signature
        {
            get
            {
                _signature = _signature == null ? new SignatureRepository(_repoContext) : _signature;
                return _signature;
            }
        }

        public IStudentRepository Student
        {
            get
            {                
                _student = _student == null ? new StudentRepository(_repoContext) : _student;
                return _student;
            }
        }

        public ITeacherRepository Teacher
        {
            get
            {
                _teacher = _teacher == null ? new TeacherRepository(_repoContext) : _teacher;
                return _teacher;
            }
        }

        public WrapperRepository(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
