using PaymentService.Domain.Common;
using System;
using System.Collections.Generic;

namespace PaymentService.Application.Commons.CustomExceptions
{

    public class BadRequestException : Exception
    {
        public BadRequestException(string msg) : base(msg)
        {

        }

        public BadRequestException(IList<Error> err, string msg): base(msg)
        {
            Errors = err;
        }
        public IList<Error> Errors { get; set; }
    }
}
