using Core.Models;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.IHelpers
{
    public interface IPaystackHelper
    {
        Task<PaystackRepsonse> GeneratePaymentParameters(TrainingCourse payData, ApplicationUserViewModel user);
        PaystackRepsonse MakeOrderPayment(TrainingCourse payData, ApplicationUserViewModel user,double amount);
        bool GetPaymentResponse(Paystack paystack);
        Task<PaystackRepsonse> VerifyPayment(Paystack payment);
        Paystack UpdateResponse(PaystackRepsonse PaystackRepsonse);
    }
}
