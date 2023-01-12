﻿using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.FinishProject
{
    public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand, bool>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IPaymentService _paymentService;

        public FinishProjectCommandHandler(IProjectRepository projectRepository, IPaymentService paymentService)
        {
            _projectRepository = projectRepository;
            _paymentService = paymentService;

        }
        public async Task<bool> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);

            project.Finish();

            //processamento de pagamento
            var paymentInfoDto = new PaymentInfoDTO(request.Id, request.CreditCardNumber, request.Cvv, request.ExpiresAt, request.FullName);

            var result = await _paymentService.ProcessPayment(paymentInfoDto);

            if (!result)
                project.SetPaymentPending();

            await _projectRepository.SaveChangeAsyng();

            return Convert.ToBoolean(Unit.Value);
        }
    }
}
