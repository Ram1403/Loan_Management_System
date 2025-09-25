namespace Loan_Management_System.Services.Payment
{
    public interface IPaymentGatewayService
    {
        Task<(bool success, string transactionId, string response)> ProcessPayment(decimal amount, string mode);
    }
}
