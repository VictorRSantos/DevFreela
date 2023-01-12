namespace DevFreela.Core.DTOs
{
    public class PaymentInfoDTO
    {
        public PaymentInfoDTO(int id, string creditCardNumber, string cvv, string expiresAt, string fullName)
        {
            Id = id;
            CreditCardNumber = creditCardNumber;
            Cvv = cvv;
            ExpiresAt = expiresAt;
            FullName = fullName;
        }

        public int Id { get; }
        public string CreditCardNumber { get; }
        public string Cvv { get; }
        public string ExpiresAt { get; }
        public string FullName { get; }
    }
}
