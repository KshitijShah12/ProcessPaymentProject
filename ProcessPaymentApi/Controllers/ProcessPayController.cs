using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcessPaymentApi.Contracts;
using ProcessPaymentApi.Entities;
using ProcessPaymentApi.Models;

namespace ProcessPaymentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessPayController : ControllerBase
    {
        private readonly IUnitOfWork _processPaymentContext;

        private readonly IUnitOfWork _processStatusContext;

        public ProcessPayController(IUnitOfWork processPaymentContext)
        {
            _processPaymentContext = processPaymentContext;
            _processStatusContext = processPaymentContext;
        }




        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("Create")]
        public async Task<ActionResult<APIResponse<ProcessPayment>>> Create(ProcessPayment process)
        {

            var processResponse = new APIResponse<ProcessPayment>();
            ProcessStatus ps = new ProcessStatus();

            try
            {
                if (!ModelState.IsValid)
                {
                    processResponse.Error = "Invalid model object";
                    processResponse.Response = HttpStatusCode.BadRequest;
                    processResponse.Result = null;
                    return processResponse;
                }

                ProcessPayment processPayment = new ProcessPayment();
                processPayment.CardHolder = process.CardHolder;
                processPayment.CreditCardNumber = process.CreditCardNumber;
                processPayment.ExpirationDate = process.ExpirationDate;
                processPayment.SecurityCode = encrypt(process.CardHolder);
                processPayment.Amount = process.Amount;

                _processPaymentContext.ProcessPaymentRepository.Create(processPayment);
                var UpdatedRowValue = await _processPaymentContext.ProcessPaymentRepository.SaveAsync();

                if (UpdatedRowValue == 1)
                {
                    
                    
                    processResponse.Response = HttpStatusCode.OK;
                    processResponse.Result = processPayment;

                    ps.CardId = processResponse.Result.Id;
                    ps.ProcessStatusName = "Processed";
                    _processStatusContext.ProcessStatusRepository.Create(ps);
                    var UpdatedRowValue1 = await _processStatusContext.ProcessStatusRepository.SaveAsync();

                    return StatusCode(200, processResponse);
                 }
                else
                {
                   
                    processResponse.Error = "Invalid model object";
                    processResponse.Response = HttpStatusCode.BadRequest;
                    processResponse.Result = null;

                    ps.CardId = process.Id;
                    ps.ProcessStatusName = "Failed";
                    _processStatusContext.ProcessStatusRepository.Create(ps);
                    var UpdatedRowValue1 = await _processStatusContext.ProcessStatusRepository.SaveAsync();

                    return BadRequest(processResponse);
                }


            }
            catch (Exception ex)
            {
                processResponse.Error = "There is some internal problem. Sorry for the inconvenience";
                processResponse.Response = HttpStatusCode.InternalServerError;
                return StatusCode(500, processResponse);
                //ex will usefull in NLog
            }
        }



        public string encrypt(string encryptString)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

    }
}