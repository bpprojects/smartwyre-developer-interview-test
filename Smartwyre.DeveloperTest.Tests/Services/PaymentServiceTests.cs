using Moq;
using Smartwyre.DeveloperTest.Interfaces.Repositories;
using Smartwyre.DeveloperTest.Service;
using Smartwyre.DeveloperTest.Types;
using System;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Services
{
    public class PaymentServiceTests
    {
        [Fact]
        public void MakePayment_NullAccount_ShouldThrowException() 
        {
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(x => x.GetAccount(It.IsAny<string>())).Returns<Account>(null);

            PaymentService paymentService = new(accountRepository.Object);

            Assert.Throws<NullReferenceException>(() => paymentService.MakePayment(new MakePaymentRequest()));
        }

        [Fact]
        public void MakePayment_InvalidPaymentSchemeBankToBankTransfer_ShouldThrowException()
        {
            var account = new Account() { AccountNumber = "123", AllowedPaymentSchemes = AllowedPaymentSchemes.BankToBankTransfer, Balance = 0, Status = AccountStatus.Live };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(account);

            PaymentService paymentService = new(accountRepository.Object);

            Assert.Throws<Exception>(() => paymentService.MakePayment(new MakePaymentRequest() { 
                Amount = 10, 
                CreditorAccountNumber = "123", 
                DebtorAccountNumber = "321", 
                PaymentDate = DateTime.Now, 
                PaymentScheme = PaymentScheme.ExpeditedPayments
            }));

        }

        [Fact]
        public void MakePayment_InvalidPaymentSchemeExpeditedPayments_ShouldThrowException()
        {
            var account = new Account() { AccountNumber = "123", AllowedPaymentSchemes = AllowedPaymentSchemes.ExpeditedPayments, Balance = 0, Status = AccountStatus.Live };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(account);

            PaymentService paymentService = new(accountRepository.Object);

            Assert.Throws<Exception>(() => paymentService.MakePayment(new MakePaymentRequest()
            {
                Amount = 10,
                CreditorAccountNumber = "123",
                DebtorAccountNumber = "321",
                PaymentDate = DateTime.Now,
                PaymentScheme = PaymentScheme.BankToBankTransfer
            }));

        }

        [Fact]
        public void MakePayment_InvalidPaymentSchemeAutomatedPaymentSystem_ShouldThrowException()
        {
            var account = new Account() { AccountNumber = "123", AllowedPaymentSchemes = AllowedPaymentSchemes.AutomatedPaymentSystem, Balance = 0, Status = AccountStatus.Live };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(account);

            PaymentService paymentService = new(accountRepository.Object);

            Assert.Throws<Exception>(() => paymentService.MakePayment(new MakePaymentRequest()
            {
                Amount = 10,
                CreditorAccountNumber = "123",
                DebtorAccountNumber = "321",
                PaymentDate = DateTime.Now,
                PaymentScheme = PaymentScheme.BankToBankTransfer
            }));

        }

        [Fact]
        public void MakePayment_ExpiditedInsufficientBalance_ShouldThrowException()
        {
            var account = new Account() { AccountNumber = "123", AllowedPaymentSchemes = AllowedPaymentSchemes.ExpeditedPayments, Balance = 0, Status = AccountStatus.Live };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(account);

            PaymentService paymentService = new(accountRepository.Object);

            Assert.Throws<Exception>(() => paymentService.MakePayment(new MakePaymentRequest()
            {
                Amount = 10,
                CreditorAccountNumber = "123",
                DebtorAccountNumber = "321",
                PaymentDate = DateTime.Now,
                PaymentScheme = PaymentScheme.ExpeditedPayments
            }));

        }

        [Fact]
        public void MakePayment_InvalidAccountStatusAutomatedPaymentSystem_ShouldThrowException()
        {
            var account = new Account() { AccountNumber = "123", AllowedPaymentSchemes = AllowedPaymentSchemes.AutomatedPaymentSystem, Balance = 0, Status = AccountStatus.Disabled };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(account);

            PaymentService paymentService = new(accountRepository.Object);

            Assert.Throws<Exception>(() => paymentService.MakePayment(new MakePaymentRequest()
            {
                Amount = 10,
                CreditorAccountNumber = "123",
                DebtorAccountNumber = "321",
                PaymentDate = DateTime.Now,
                PaymentScheme = PaymentScheme.AutomatedPaymentSystem
            }));
        }

        [Fact]
        public void MakePayment_ValidAccount_Success()
        {
            var account = new Account() { AccountNumber = "123", AllowedPaymentSchemes = AllowedPaymentSchemes.AutomatedPaymentSystem, Balance = 0, Status = AccountStatus.Live };
            var accountRepository = new Mock<IAccountRepository>();
            accountRepository.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(account);

            PaymentService paymentService = new(accountRepository.Object);

            paymentService.MakePayment(new MakePaymentRequest()
            {
                Amount = 10,
                CreditorAccountNumber = "123",
                DebtorAccountNumber = "321",
                PaymentDate = DateTime.Now,
                PaymentScheme = PaymentScheme.AutomatedPaymentSystem
            });

            accountRepository.Verify(x => x.UpdateAccount(It.IsAny<Account>()), Times.Once);
        }
    }
}
