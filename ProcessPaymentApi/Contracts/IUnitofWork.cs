
using ProcessPaymentApi.Entities;

namespace ProcessPaymentApi.Contracts
{
    public interface IUnitOfWork
    {
        IRepositoryBase<ProcessPayment> ProcessPaymentRepository { get; }

        IRepositoryBase<ProcessStatus> ProcessStatusRepository { get; }
        void Save();
    }
}