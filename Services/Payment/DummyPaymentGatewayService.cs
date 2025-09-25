using System;

namespace Loan_Management_System.Services.Payment
{
    public class DummyPaymentGatewayService : IPaymentGatewayService
    {
        public Task<(bool success, string transactionId, string response)> ProcessPayment(decimal amount, string mode)
        {
            // Simulate Razorpay/PayPal/UPI response
            var transactionId = $"DUMMY-{Guid.NewGuid()}";
            var response = $"Dummy {mode} payment of {amount} processed successfully.";

            return Task.FromResult((true, transactionId, response));
        }
    }
}
