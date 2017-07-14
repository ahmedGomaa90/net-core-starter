using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using CoWorking.Data.Repositories;
using CoWorking.Data.Repositories.Repositories;
using CoWorking.Domain.Entities;
using CoWorking.Business.Managers;
using CoWorking.Domain.BusinessConracts.Managers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CoWorking.Services.WebApi.Models;
using CoWorking.Data.Context;

namespace CoWorking.Services.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController()//UserManager<IdentityUser> UserManager)
        {
            _userManager = new UserManager<IdentityUser>(
            new UserStore<IdentityUser>(new CoWorkingIdentityContext()),
            null,
            new PasswordHasher<IdentityUser>(),
            new List<UserValidator<IdentityUser>>(),
            new List<PasswordValidator<IdentityUser>>(),
            null,
            new IdentityErrorDescriber(),
            null, null);
        }

        [AllowAnonymous]
        // [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]UserBindingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new IdentityUser
            {
                UserName = model.UserName
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            var errorResult = GetErrorResult(result);

            if(errorResult != null)
                return errorResult;

            return Ok(user.Id);
        }



        private IActionResult GetErrorResult(IdentityResult result)
        {
            // if (result == null)
            // {
            //     return InternalServerError();
            // }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}