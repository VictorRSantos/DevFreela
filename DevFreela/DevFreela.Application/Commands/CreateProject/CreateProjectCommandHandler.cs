using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CreateProject
{
    // CommandHandler vai tratar as informações
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
    {
        public Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
