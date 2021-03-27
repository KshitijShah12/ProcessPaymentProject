using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPaymentApi.Models
{
    public class ProcessPaymentModel
    {
       
        public long Id { get; set; }
       
        public string CreditCardNumber { get; set; }

       
        public string   CardHolder { get; set; }

        
        public DateTime ExpirationDate { get; set; }

        
        public string SecurityCode { get; set; }

        
        public string Amount { get; set; }


    }
}
