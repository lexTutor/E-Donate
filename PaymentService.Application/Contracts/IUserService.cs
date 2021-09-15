using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PaymentService.Domain.Common;
using PaymentService.Domain.DataTransfer.AccountDtos;
using PaymentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.Contracts
{
    public interface IUserService
    {
        Task<Response<UserResponseDto>> Register(RegisterationRequestDto model, UserManager<AppUser> userManager);
        Task<bool> ChangePassword(string userId, ChangePasswordDto model, UserManager<AppUser> userManager);
        Task<bool> AssignRoles(string userId, List<string> roles, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager);
    }
}
