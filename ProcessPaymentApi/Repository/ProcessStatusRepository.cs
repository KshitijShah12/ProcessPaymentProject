using ProcessPaymentApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPaymentApi.Repository
{
    public class ProcessStatusRepository : RepositoryBase<ProcessStatus>
    {
        public ProcessStatusRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}
