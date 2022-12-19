using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Smartwyre.DeveloperTest.Infrastructure.Repositories;
using Smartwyre.DeveloperTest.Interface.Services;
using Smartwyre.DeveloperTest.Interfaces.Repositories;
using Smartwyre.DeveloperTest.Service;
using System;

namespace Smartwyre.DeveloperTest.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IPaymentService, PaymentService>();
                    services.AddTransient<IAccountRepository, AccountRepository>();
                })
                .Build();
            try
            {
                var paymentService = ActivatorUtilities.CreateInstance<PaymentService>(host.Services);
                paymentService.MakePayment(new Types.MakePaymentRequest()
                {
                    Amount = 1000,
                    CreditorAccountNumber = "123",
                    DebtorAccountNumber = "321",
                    PaymentDate = DateTime.Now,
                    PaymentScheme = Types.PaymentScheme.AutomatedPaymentSystem
                });
                Console.WriteLine($"Payment Successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

