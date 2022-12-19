using Moq;
using Smartwyre.DeveloperTest.Infrastructure.Repositories;
using Smartwyre.DeveloperTest.Interfaces.Repositories;
using Smartwyre.DeveloperTest.Service;
using Smartwyre.DeveloperTest.Types;
using System;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Repositories
{
    public class AccountRepositoryTests
    {
        [Fact]
        public void GetAccount_ValidAccount_Success()
        {
            AccountRepository accountRepository = new AccountRepository();

            Assert.NotNull(accountRepository.GetAccount("123"));
        }
    }
}
