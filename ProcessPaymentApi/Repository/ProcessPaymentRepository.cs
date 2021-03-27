using ProcessPaymentApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPaymentApi.Repository
{
    public class ProcessPaymentRepository : RepositoryBase<ProcessPayment>
    {
        public ProcessPaymentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}
