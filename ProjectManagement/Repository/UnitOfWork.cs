using AutoMapper;
using ProjectManagement.Data;
using ProjectManagement.Repository.IRepository;

namespace ProjectManagement.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _dBContext;
        private readonly IMapper _mapper;
        private IProjectService _projectService;

        public UnitOfWork(ApplicationDBContext dBContext, IMapper mapper)
        {
            _dBContext = dBContext;
            _mapper = mapper;
        }

        public IProjectService ProjectService
            => _projectService ??= new ProjectService(_dBContext, _mapper);

        public async Task<bool> SaveAsync()
        {
            return await _dBContext.SaveChangesAsync() > 0;
        }
    }

}
