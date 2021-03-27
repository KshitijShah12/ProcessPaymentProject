using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
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

                _processPaymentContext.ProcessPaymentRepository.Create(process);
                var UpdatedRowValue = await _processPaymentContext.ProcessPaymentRepository.SaveAsync();

                if (UpdatedRowValue == 1)
                {
                    
                    
                    processResponse.Response = HttpStatusCode.OK;
                    processResponse.Result = process;

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
    }
}