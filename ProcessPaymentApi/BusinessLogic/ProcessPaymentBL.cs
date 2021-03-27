using Microsoft.AspNetCore.Mvc;
using ProcessPaymentApi.Contracts;
using ProcessPaymentApi.Entities;
using ProcessPaymentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProcessPaymentApi.BusinessLogic
{
    public class ProcessPaymentBL : ControllerBase
    {
        private readonly IUnitOfWork _processPaymentContext1;



        public ProcessPaymentBL(IUnitOfWork processPaymentContext2)
        {
            _processPaymentContext1 = processPaymentContext2;
        }


        
        public async Task<ActionResult<APIResponse<ProcessPayment>>> SaveProcesspayment(ProcessPayment processPayment)
        {
            var processResponse = new APIResponse<ProcessPayment>();
            try
            {
                _processPaymentContext1.ProcessPaymentRepository.Create(new ProcessPayment()
                {
                    CardHolder = processPayment.CardHolder,
                    CreditCardNumber = processPayment.CreditCardNumber,
                    ExpirationDate = processPayment.ExpirationDate,
                    SecurityCode = processPayment.SecurityCode,
                    Amount = processPayment.Amount
                });

                var UpdatedRowValue = await _processPaymentContext1.ProcessPaymentRepository.SaveAsync();

                if (UpdatedRowValue == 1)
                {
                    processResponse.Response = HttpStatusCode.OK;
                    processResponse.Result = processPayment;
                    return StatusCode(200, processResponse);

                }
                else
                {
                    processResponse.Error = "Invalid model object";
                    processResponse.Response = HttpStatusCode.BadRequest;
                    processResponse.Result = null;
                    return BadRequest(processResponse);
                }

            }
            catch (Exception ex)
            {

                throw;
            }

        }

    }
}
