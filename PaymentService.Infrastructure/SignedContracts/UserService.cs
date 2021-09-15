using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PaymentService.Application.Contracts;
using PaymentService.Domain.Common;
using PaymentService.Domain.DataTransfer.AccountDtos;
using PaymentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace PaymentService.Infrastructure.SignedContracts
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        public UserService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<bool> AssignRoles(string userId, List<string> rolesToAssign, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (rolesToAssign is null || rolesToAssign.Count < 1)
                return false;

            var appUser = await userManager.FindByIdAsync(userId);
            if (appUser == null)
                return false;

            var rolesNotExist = rolesToAssign.Except(roleManager.Roles.Select(x => x.Name)).ToArray();

            if (rolesNotExist.Count() > 0)
                return false;

            IdentityResult removeResult = await userManager.RemoveFromRolesAsync(appUser.Id, rolesNotExist.ToArray());


            if (!removeResult.Succeeded)
                return false;

            ///assign user to the new roles
            IdentityResult addResult = await userManager.AddToRolesAsync(appUser.Id, rolesToAssign.ToArray());

            if (!addResult.Succeeded)
                return false;
            return true;
        }

        public async Task<Response<UserResponseDto>> Register(RegisterationRequestDto model, UserManager<AppUser> userManager)
        {
            Response<UserResponseDto> response = new Response<UserResponseDto>();
            var user = _mapper.Map<AppUser>(model);

            //set the IsDeleted property to false
            user.IsDeleted = false;

            IdentityResult result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                response.Message = "User created successfully";
                response.IsSuccess = true;
                response.Data = _mapper.Map<UserResponseDto>(user);
                return response;
            }

            response.Message = "User was not created";
            response.Errors = new List<Error>();
            foreach (var err in result.Errors)
            {
                response.Errors.Add(new Error { Others = err });
            }
            return response;
        }

        public async Task<bool> ChangePassword(string userId, ChangePasswordDto model, UserManager<AppUser> userManager)
        {
            IdentityResult result = await userManager.ChangePasswordAsync(userId , model.OldPassword, model.NewPassword);

            return result.Succeeded;
        }
    }
}
