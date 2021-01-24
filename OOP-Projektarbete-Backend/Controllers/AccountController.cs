using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OOP_Projektarbete_Backend.DTOs;
using OOP_Projektarbete_Backend.Models;
using OOP_Projektarbete_Backend.Services.Interfaces;
using OOP_Projektarbete_Backend.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Projektarbete_Backend.Controllers
{
    [ApiController]
    [EnableCors("CORSPolicy")]
    [Route("api/[controller]")]

    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration,
            IUserService userService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserInfo model)
        {
            var user = new User { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return BuildToken(model);
            }
            else
            {
                return BadRequest("Failed to create user");
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo userInfo)
        {
            var result = await _signInManager.PasswordSignInAsync(userInfo.Email,
                userInfo.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return BuildToken(userInfo);
            }
            else
            {
                return BadRequest("Wrong username or password");
            }
        }

        [HttpGet("GetUsers")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> GetUsers()
        {
            var result = await _userService.GetAllAsync(User.Identity.Name);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var users = _mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(result.Resource);
            return Ok(users);
            
        }


        private UserToken BuildToken(UserInfo userInfo)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userInfo.Email),
                new Claim(ClaimTypes.Email, userInfo.Email),
                new Claim(ClaimTypes.NameIdentifier, userInfo.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddYears(1);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Username = userInfo.Email,
                Expiration = expiration
            };
        }
    }
}