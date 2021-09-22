using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PaymentService.API.App_Start;
using PaymentService.Application.Contracts;
using PaymentService.Domain.DataTransfer.AccountDtos;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PaymentService.API.Controllers
{
    [RoutePrefix("api/v1/Account")]
    public class AccountController : ApiController
    {
        private ApplicationUserManager UserManager;
        private ApplicationRoleManager RoleManager;
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }


        //Usermanager Property
        public ApplicationUserManager _userManager
        { 
            get
            {
                return UserManager ?? HttpContext.Current.Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            } 
            set 
            {
                UserManager = value; 
            }
        }

        public ApplicationRoleManager _roleManager
        {
            get
            {
                return RoleManager ?? HttpContext.Current.Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                RoleManager = value;
            }
        }


       //POST api/Account/Register
       [AllowAnonymous]
       [Route("Register")]
        public async Task<HttpResponseMessage> Register(RegisterationRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var result = await _userService.Register(model, _userManager);

            if (result.IsSuccess)
                return Request.CreateResponse(HttpStatusCode.OK, result);
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }

        //POST api/Account/assignRoles
        [Authorize]
        [Route("assignRoles")]
        public async Task<HttpResponseMessage> AssignRoles(List<string> roles)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var xx = User.Identity.GetUserId();
            var result = await _userService.AssignRoles(User.Identity.GetUserId(), roles, _userManager, _roleManager);

            if (result)
                return Request.CreateResponse(HttpStatusCode.NoContent);
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        //POST api/Account/assignRoles
        [Authorize]
        [Route("change-password")]
        public async Task<HttpResponseMessage> ChangePassword(ChangePasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var result = await _userService.ChangePassword(User.Identity.GetUserId(), model, _userManager);

            if (result)
                return Request.CreateResponse(HttpStatusCode.NoContent);
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
