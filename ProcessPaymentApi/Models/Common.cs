using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProcessPaymentApi.Models
{
    public class APIResponse<T>
    {
        public HttpStatusCode Response { get; set; }

        public string Error { get; set; }

        public T Result { get; set; }

        internal string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
