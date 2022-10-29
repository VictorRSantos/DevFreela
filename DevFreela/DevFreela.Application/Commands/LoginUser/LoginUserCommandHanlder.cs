using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.LoginUser
{
    public class LoginUserCommandHanlder : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        public LoginUserCommandHanlder(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            // Utilizar o mesmo algoritmo para criar o hash dess senha
            var passwrodHash = _authService.ComputeSha256Hash(request.Password);

            // Buscar no  meu banco de dados um User que tenha meu e-mail e minha senha em formatado hash
            var user = await _userRepository.GetUserByEmailAndPasswordAsync(request.Email, passwrodHash);

            // Se não existir, erro no login
            if (user == null)
            {
                return null;
            }

            // Se existir, gera o token usuando os dados do usuário
            var token = _authService.GenerateJwtToken(user.Email, user.Role);

            var loginViewViewModel = new LoginUserViewModel(user.Email,token);

            return loginViewViewModel;
        }
    }
}
