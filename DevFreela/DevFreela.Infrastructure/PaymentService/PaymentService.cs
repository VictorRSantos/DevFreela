﻿using DevFreela.Core.DTOs;
using DevFreela.Core.Services;
using System.Text;
using System.Text.Json;

namespace DevFreela.Infrastructure.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IMessageBusService _messageBusService;
        private readonly string QUEUE_NAME = "Payments";
        public PaymentService(IMessageBusService messageBusService)
        {
            _messageBusService = messageBusService;

        }

        public void ProcessPayment(PaymentInfoDTO paymentInfoDTO)
        {
            var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDTO);

            var paymentInfoBytes = Encoding.UTF8.GetBytes(paymentInfoJson);

            _messageBusService.Publish(QUEUE_NAME, paymentInfoBytes);
        }
    }
}
