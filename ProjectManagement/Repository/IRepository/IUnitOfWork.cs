namespace ProjectManagement.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IProjectService ProjectService { get; }
        Task<bool> SaveAsync();
    }
}
