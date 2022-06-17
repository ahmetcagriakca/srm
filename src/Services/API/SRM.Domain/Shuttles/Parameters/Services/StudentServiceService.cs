using Fix.Data;
using SRM.Data.Models.Shuttles.Parameters;
using SRM.Domain.Parameters.Services;
using System.Linq;

namespace SRM.Domain.Individuals.Parameters.Services
{
    public class StudentServiceService : ParameterService<StudentService>, IStudentServiceService
    {
        //private readonly IAccountService accountService;

        public StudentServiceService(
            IRepository<StudentService> repository
            //IAccountService accountService
            ) : base(repository)
        {
            //this.accountService = accountService;
        }

        public override StudentService GetById(int id)
        {
            return _repository.Table./*Include(en => en.Driver).*/FirstOrDefault(en => en.Id == id);
        }

        public override IQueryable<StudentService> Get()
        {
            return _repository.Table/*.Include(en => en.Driver)*/;
        }


        public void Create(StudentService entity, long driver)
        {
            //entity.Driver = accountService.GetUser(driver);

            _repository.Add(entity);
        }

        public void Update(StudentService entity, long driver)
        {
            //entity.Driver = accountService.GetUser(driver);

            _repository.Update(entity);
        }
    }
}
