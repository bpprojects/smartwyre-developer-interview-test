using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Account GetAccount(string accountNumber);

        void UpdateAccount(Account account);
    }
}
