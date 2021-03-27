using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPaymentApi.Entities
{
    [Table("ProcessPayment_tbl")]
    public class ProcessPayment
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [CreditCardAttribute]
        public string CreditCardNumber { get; set; }

        [Required]
        public string CardHolder { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }


        public string SecurityCode { get; set; }

        [Required]
        public string Amount { get; set; }

        

        public ProcessStatus ProcessStatus { get; set; }
    }
}
