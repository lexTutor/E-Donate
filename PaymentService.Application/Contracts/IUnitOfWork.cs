using PaymentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.Contracts
{
    /// <summary>
    /// A contract that enables a class to implement a unit of work repository pattern
    /// Which enable changes to be saved in a bulk operation
    /// </summary>
    public interface IUnitOfWork
    {
         IBaseRepository<Payment> PaymentRepository { get; }
         IBaseRepository<PaymentStatus> PaymentStatusRepository { get; }
        Task<bool> SaveChangesAsync();
    }
}
