using Moq;
using NUnit.Framework;
using ProcessPaymentApi.BusinessLogic;
using ProcessPaymentApi.Contracts;
using ProcessPaymentApi.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestProcessPayment.TestMethods
{
    [TestFixture]
    public class ProcessPaymentTestBL
    {
        //private readonly IUnitOfWork _processPaymentContext;
        
        private Mock<IUnitOfWork> _processPaymentContext;

     
      
        [Test]
        public void TestProcessPayment()
        {
            var mockRepo = new Mock<IUnitOfWork>();

            string CardHolder = "K";
            string CreditCardNumber = "1234121212451236";
            DateTime ExpirationDate = DateTime.Now;
            string SecurityCode = "123";
            string Amount = "1234";

            var processPayment = new ProcessPayment()
            {
                CardHolder = CardHolder,
                CreditCardNumber = CreditCardNumber,
                ExpirationDate = ExpirationDate,
                SecurityCode = SecurityCode,
                Amount = Amount
            };

            var processPaymentBL = new ProcessPaymentBL(mockRepo.Object);
            mockRepo.Setup(p => p.ProcessPaymentRepository.Create(processPayment));
            mockRepo.Setup(repo => repo.ProcessPaymentRepository.SaveAsync()).Verifiable();
            
            var savepament = processPaymentBL.SaveProcesspayment(processPayment);
        }
    }
}
