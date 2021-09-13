using PaymentService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.Contracts
{
    public interface IRequestHandler
    {
        Task<TRes> SendAsync<TReq, TRes>(TReq requestData, string url, string authorization = null);
        Task<TRes> GetAsync<TRes>(string url, string authorization = null);
    }
}
