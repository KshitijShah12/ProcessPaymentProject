using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPaymentApi.Entities
{
    public class ProcessStatus
    {
        [Key]
        public long ProcessStatusId { get; set; }

        public string ProcessStatusName { get; set; }
        public long CardId { get; set; }

        public ProcessPayment ProcessPayment { get; set; }



    }
}
