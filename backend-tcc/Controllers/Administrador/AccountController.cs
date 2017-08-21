using backend_tcc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using backend_tcc.api.Models.Administrador;

namespace backend_tcc.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        /// <summary>
        /// Register a new user on application
        /// </summary>
        /// <param name="user">New user to register</param>
        /// <remarks>Adds new user to application and grant access</remarks>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [AllowAnonymous]
        [Route("Register")]
        [ResponseType(typeof(RegisterBindingModel))]
        [HttpPost]
        public async Task<IHttpActionResult> Register(RegisterBindingModel user)
        {
            //IMPLEMENTATION OF CODE GOES HERE!!
            throw new NotImplementedException();
        }

        /// <summary>
        /// Realizar a autenticação dos usuários
        /// </summary>
        /// <param name="user">Usuário</param>
        /// <param name="password">Senha</param>
        /// <returns>Token de acesso a API</returns>
        [AllowAnonymous]
        [Route("Authenticate")]
        [ResponseType(typeof(RegisterBindingModel))]
        [HttpPost]
        public async Task<UserTokenModel> Authenticate(string user, string password)
        {
            //IMPLEMENTATION OF CODE GOES HERE!!
            throw new NotImplementedException();

            var plainTextSecurityKey = "tcc2017";
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(plainTextSecurityKey));

            var signingCredentials = new SigningCredentials(signingKey,
                SecurityAlgorithms.HmacSha256Signature);

            var claimsIdentity = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, "myemail@myprovider.com"),
                new Claim(ClaimTypes.Role, "Administrator"),
            }, "Custom");

            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = "http://my.tokenissuer.com",
                Audience = "http://my.website.com",
                Subject = claimsIdentity,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var plainToken = tokenHandler.CreateToken(securityTokenDescriptor);
            var signedAndEncodedToken = tokenHandler.WriteToken(plainToken);

            return new UserTokenModel() { Token = signedAndEncodedToken };
        }        
    }
}
