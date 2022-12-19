using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Interface.Services
{
    public interface IPaymentService
    {
        void MakePayment(MakePaymentRequest request);
    }
}
