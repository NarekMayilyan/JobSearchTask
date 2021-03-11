using AutoMapper;
using JobSearch.DAL.Entities.Users;
using JobSearch.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JobSearch.WEB.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public AccountController(IMapper mapper,
            UserManager<User> userManager)
        {
            this.mapper = mapper;
            this.userManager = userManager;
        }

        private async Task CookieLogIn(User user)
        {
            var claimsPrincipal = GetUserClaims(user);
            var authProperties = GetAuthenticationProperties();

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);
        }

        private ClaimsPrincipal GetUserClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Sid, user.Id.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            return claimsPrincipal;
        }

        private AuthenticationProperties GetAuthenticationProperties()
        {
            var authProperties = new AuthenticationProperties()
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(1),
                AllowRefresh = true
            };

            return authProperties;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] RegisterModel model)
        {
            User user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                user = mapper.Map<RegisterModel, User>(model);
                await userManager.CreateAsync(user, model.Password);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.FirstName),
                    new Claim(ClaimTypes.Surname,user.LastName)
                };
                await userManager.AddClaimsAsync(user, claims);

                await CookieLogIn(user);
                return Ok();
            }
            return BadRequest("Email Already Exist");
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);

            if (user != null)
            {
                if (await userManager.CheckPasswordAsync(user, model.Password))
                {
                    await CookieLogIn(user);
                    return Ok();
                }
                else
                {
                    return BadRequest("Wrong User Name Or Password");
                }
            }
            return BadRequest("Wrong User Name Or Password");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Signout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

    }
}
