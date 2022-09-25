using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.UpdateProject
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Unit>
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;
        public UpdateProjectCommandHandler(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaSc");
        }

        public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == request.Id);

            project.Update(request.Title, request.Description, request.TotalCost);

            await _dbContext.SaveChangesAsync();

            #region Update com Dapper
            //using (var sqlConnetion = new SqlConnection(_connectionString))
            //{
            //    sqlConnetion.Open();

            //    var script = @"UPDATE Projects SET Status = @status, StartedAt = @startedAt WHERE Id = @id";

            //    sqlConnetion.Execute(script, new { status = project.Status, startedAt = project.StartedAt, id = id });
            //}

            #endregion


            return Unit.Value;
        }
    }
}
