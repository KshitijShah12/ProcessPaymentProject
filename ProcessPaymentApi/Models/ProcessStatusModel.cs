using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPaymentApi.Models
{
    public class ProcessStatusModel
    {
        public long ProcessStatusId { get; set; }

        public string ProcessStatusName { get; set; }

        public long CardId { get; set; }
    }
}
