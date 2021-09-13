using FluentValidation;
using PaymentService.Domain.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Application.Commons.Validators
{
    public class RecievePaymentValidator : AbstractValidator<RecievePaymentDto>
    {
        public RecievePaymentValidator()
        {
            RuleFor(model => model.Amount)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Amount cannot be empty")
                .GreaterThan(0).WithMessage("Amount must be greater tha  zero");

            RuleFor(model => model.Email)
                .Cascade(CascadeMode.Stop)
                .EmailAddress()
                .NotEmpty().WithMessage("Email field is required");

        }

        private bool ValidDate(DateTime date)
        {
            if (date < DateTime.Now)
            {
                return false;
            }

            return true;
        }
    }
}
