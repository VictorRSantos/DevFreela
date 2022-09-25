using MediatR;
using System.Net;

namespace DevFreela.Application.Commands.CreateProject
{
    // Command vai conter as informações para cadastrar um projeto
    public class CreateProjectCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int IdClient { get; set; }
        public int IdFreelancer { get; set; }
        public decimal TotalCost { get; set; }
    }
}
