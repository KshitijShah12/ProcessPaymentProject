using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPaymentWeb.Models
{
    [Table("ProcessPayment_tbl")]
    public class ProcessPayment
    {
        public long Id { get; set; }
        [Display(Name = "Credit Card Number")]
        [Required]
        public string CreditCardNumber { get; set; }

        [Display(Name = "Card Holder Name")]
        [Required]
        public string CardHolder { get; set; }
        [Display(Name = "Expiry Date")]
        [Required]
        public DateTime ExpirationDate { get; set; }


        public string SecurityCode { get; set; }
        
        [Required]
        public string Amount { get; set; }
    }
}
