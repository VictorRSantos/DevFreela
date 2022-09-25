using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.StartProject
{
    public class StartProjectCommandHandler : IRequestHandler<StartProjectCommand, Unit>
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public StartProjectCommandHandler(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration.GetConnectionString("DevFreelaSc");

        }
        public async Task<Unit> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {

            using (var sqlConnetion = new SqlConnection(_connectioString))
            {
                sqlConnetion.Open();

                var script = @"UPDATE Projects SET Status = @status, StartedAt = @startedAt WHERE Id = @id";

                sqlConnetion.Execute(script, new { status = project.Status, startedAt = project.StartedAt, id = id });
            }
            

            return Unit.Value;
        }
    }
}
