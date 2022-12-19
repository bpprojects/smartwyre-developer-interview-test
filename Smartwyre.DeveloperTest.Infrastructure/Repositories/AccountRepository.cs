using Smartwyre.DeveloperTest.Interfaces.Repositories;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public Account GetAccount(string accountNumber)
        {
            // Access database to retrieve account, code removed for brevity 
            return new Account() { AccountNumber = "123", AllowedPaymentSchemes = AllowedPaymentSchemes.AutomatedPaymentSystem, Balance = 100, Status = AccountStatus.Disabled};
        }

        public void UpdateAccount(Account account)
        {
            // Update account in database, code removed for brevity
        }
    }
}
