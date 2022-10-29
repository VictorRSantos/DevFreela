using MediatR;
using System;

namespace DevFreela.Application.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<int>
    {
        public CreateUserCommand(string fullName, string password, string email, DateTime birthDate)
        {
            FullName = fullName;
            Password = password;
            Email = email;
            BirthDate = birthDate;
        }

        public string FullName { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
    }
}
