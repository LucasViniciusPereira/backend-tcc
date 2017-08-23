using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using backend_tcc.api.Models.Administrador;
using backend_tcc.bs.administrador.Class;
using backend_tcc.lib.repository.Repository;
using backend_tcc.Models;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using backend_tcc.lib.repository.Class;

namespace backend_tcc.Controllers
{
    /// <summary>
    /// Api de Autenticação
    /// </summary>
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
        [ResponseType(typeof(UserTokenModel))]
        [HttpPost]
        public UserTokenModel Authenticate(string user, string password)
        {
            //IMPLEMENTATION OF CODE GOES HERE!!
            //throw new NotImplementedException();

            var plainTextSecurityKey = "PosGraduacaoTcc2017WilsonDonizetti";
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(plainTextSecurityKey));

            var signingCredentials = new SigningCredentials(signingKey,
                SecurityAlgorithms.HmacSha256Signature);

            UnitOfWork unitOfWork = new UnitOfWork(new EFDbContext());
            var usuario = unitOfWork.Repository<Usuario>().Table.Where(c => c.UsuarioID == user && c.Senha == password).SingleOrDefault();
            if (usuario != null && usuario.Ativo)
            {
                var claimsIdentity = new ClaimsIdentity(new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.NameIdentifier, usuario.Nome),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Role, "User"),
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

                return new UserTokenModel() { Success = true, Token = signedAndEncodedToken, Message = "Autenticado com sucesso" };
            }
            else
                return new UserTokenModel() { Success = false, Message = "Não foi possível efeturar o login" };
        }
    }
}
