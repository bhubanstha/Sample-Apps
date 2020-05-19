using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApi.Data;
using CoreApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ViewModel;
using Identity = Microsoft.AspNetCore.Identity;

namespace CoreApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private TokenProvider tokenProvider;
        private IConfiguration _config;
        private ApplicationContext _dbContext;
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        public AccountController(IConfiguration configuration, ApplicationContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _dbContext = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = configuration;
            tokenProvider = new TokenProvider(_config);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody]RegisterViewModel reUser)
        {
            if(ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    Email = reUser.Email,
                    EmailConfirmed = true,
                    UserName = reUser.UserName
                };

                IdentityResult result = await _userManager.CreateAsync(user, reUser.Password);

                if(result.Succeeded)
                {
                    return Ok("User Created Successfully");
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't create user at this time");
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByEmailAsync(model.Email);
               Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);
                if(result ==  Identity.SignInResult.Success)
                {
                    string _token = tokenProvider.GenerateToken(user);
                    if (_token != null)
                    {
                        //Save token in session object
                        HttpContext.Response.Cookies.Append("JWToken", _token);
                        HttpContext.Session.SetString("JWToken", _token);
                    }
                    return StatusCode(StatusCodes.Status200OK, _token);
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Invalid user");
        }

    }
}