using Smartwyre.DeveloperTest.Interface.Services;
using Smartwyre.DeveloperTest.Interfaces.Repositories;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountRepository _accountRepository;

        public PaymentService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void MakePayment(MakePaymentRequest request)
        {
            Account account = _accountRepository.GetAccount(request.DebtorAccountNumber);
            if (account == null)
            {
                throw new NullReferenceException();           
            }

            validatePayment(account, request);

            account.Balance -= request.Amount;
            _accountRepository.UpdateAccount(account);
        }

        private void validatePayment(Account account, MakePaymentRequest request)
        {
            string invalidPaymentScheme = "Payment scheme not allowed";

            switch (request.PaymentScheme)
            {
                case PaymentScheme.BankToBankTransfer:
                    if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.BankToBankTransfer))
                    {
                        throw new Exception(invalidPaymentScheme);
                    }
                    break;

                case PaymentScheme.ExpeditedPayments:
                    if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.ExpeditedPayments))
                    {
                        throw new Exception(invalidPaymentScheme);
                    }
                    else if (account.Balance < request.Amount)
                    {
                        throw new Exception("Insufficient Account Balance");
                    }
                    break;

                case PaymentScheme.AutomatedPaymentSystem:
                    if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.AutomatedPaymentSystem))
                    {
                        throw new Exception(invalidPaymentScheme);
                    }
                    else if (account.Status != AccountStatus.Live)
                    {
                        throw new Exception("Account is not yet live");
                    }
                    break;
            }
        }
    }
}