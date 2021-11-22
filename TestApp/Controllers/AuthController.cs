using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TestApp.DTOs;
using TestApp.Service;

namespace TestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private IMediator _mediator;

        public AuthController(IConfiguration configuration, IUserService userService, IMediator mediator)
        {
            _configuration = configuration;
            _userService = userService;
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost(nameof(Auth))]
        public IActionResult Auth([FromBody] UserDto user)
        {
            bool isValid = _userService.IsValidUserInformation(user);

            if (isValid)
            {
                var tokenString = GenerateJwtToken(user.UserName);
                return Ok(new
                {
                    Token = tokenString,
                    Message = "Success"
                }); ;
            }
            return BadRequest("Please pass the valid Username and Password");
        }


        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(nameof(GetResult))]
        public IActionResult GetResult()
        {
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                var claims = identity.Claims;
                var expiresOn = claims.FirstOrDefault(c => c.Type == "ExpiresOn")?.Value;
            }
            return Ok("API Authenticated");
        }

        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(nameof(GetExpiryDate))]
        public IActionResult GetExpiryDate()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var expiresOn = "";
            IEnumerable<Claim> claims = new List<Claim>();

            if (identity != null)
            {
                claims = identity.Claims;
                expiresOn = claims.FirstOrDefault(c => c.Type == "ExpiresOn")?.Value;
            }
            return Ok("Token will expire on " + expiresOn);
        }

        private string GenerateJwtToken(string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
            var expiresOn = DateTime.Now.AddHours(1);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = expiresOn,
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", userName),
                    new Claim("IssuedOn", DateTime.Now.ToString()),
                    new Claim("ExpiresOn", expiresOn.ToString())
                }),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = tokenDescriptor.Expires
            };
            Response.Cookies.Append("CustomJwtCookie", "Cookie_Validity", cookieOptions);

            return tokenString;
        }
    }
}
