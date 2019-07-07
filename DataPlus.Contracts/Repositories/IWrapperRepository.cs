namespace DataPlus.Contracts.Repositories
{
    public interface IWrapperRepository
    {
        IStudentRepository Student { get; }
        ITeacherRepository Teacher { get; }
        ICourseRepository Course { get; }
        ISignatureRepository Signature { get; }
        void Save();
    }
}
