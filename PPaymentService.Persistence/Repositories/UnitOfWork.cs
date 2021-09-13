using PaymentService.Application.Contracts;
using PaymentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PaymentDbContext _ctx;

        public UnitOfWork(PaymentDbContext context)
        {
            _ctx = context;
        }

        public IBaseRepository<Payment> PaymentRepository => new BaseRepository<Payment>(_ctx);

        public IBaseRepository<PaymentStatus> PaymentStatusRepository => new BaseRepository<PaymentStatus>(_ctx);

        public async Task<bool> SaveChangesAsync()
        {
            return await _ctx.SaveChangesAsync() > 0;
        }
    }
}
