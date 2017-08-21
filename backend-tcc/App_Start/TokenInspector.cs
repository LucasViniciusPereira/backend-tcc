using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.IdentityModel.Tokens;

namespace backend_tcc.api.App_Start
{
    public class TokenInspector : DelegatingHandler
    {
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //log4net.GlobalContext.Properties["ClientIP"] = request.GetClientIP();

            //Evitar que requisições específicas e de documentação SWASHBUCKLE sejam verificadas.
            if (request.RequestUri.Segments.Any(s => s.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Contains("swagger"))
                || request.RequestUri.Segments.Any(s => s.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Contains("help"))
                || request.RequestUri.ToString().ToLower().Contains("account/authenticate"))
                return await base.SendAsync(request, cancellationToken);

            //TokenMessage tokenMsg;
            HttpResponseMessage reply;

            IEnumerable<string> keys = null;
            if (request.Headers.TryGetValues("Token", out keys) && !string.IsNullOrEmpty(keys.First()))
            {
                try
                {
                    string token = keys.First();

                    var plainTextSecurityKey = "tcc2017";
                    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(plainTextSecurityKey));

                    var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                    var tokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidAudiences = new string[]
                    {
                        "http://my.website.com",
                        "http://my.otherwebsite.com"
                    },
                        ValidIssuers = new string[]
                    {
                        "http://my.tokenissuer.com",
                        "http://my.othertokenissuer.com"
                    },
                        IssuerSigningKey = signingKey
                    };

                    SecurityToken validatedToken;

                    tokenHandler.ValidateToken(token,
                        tokenValidationParameters, out validatedToken);


                    var teste = tokenHandler.ReadToken(token);


                    //ClaimsPrincipal principal = IdentityStore.CriarUsuarioClaim(user, "CustomAuthToken");

                    //bool requestIPMatchesTokenIP = true; //token.IP.Equals(request.GetClientIP());

                    //if (user == null || principal == null || !requestIPMatchesTokenIP)
                    //{
                    //    #region Identidade do usuário ou máquina cliente inválida.

                    //    tokenMsg = new TokenMessage
                    //    {
                    //        Success = false,
                    //        Token = null,
                    //        Message = $"{(short)HttpStatusCode.Unauthorized} - {HttpStatusCode.Unauthorized.ToString()} (Identidade do usuário ou máquina cliente inválida.)"
                    //    };

                    //    reply = request.CreateResponse(HttpStatusCode.Unauthorized, tokenMsg);

                    //    log.Info($"{token.User} - {token.IP} - {request.RequestUri.ToString()} - {tokenMsg.Message}");

                    //    return await Task.FromResult(reply);

                    //    #endregion
                    //}
                    //else
                    //{
                    //    #region Se a data de expiração é anterior ao dia de hoje.

                    //    if (DateTime.Now.CompareTo(token.Expire) > 0)
                    //    {
                    //        tokenMsg = new TokenMessage
                    //        {
                    //            Success = false,
                    //            Token = null,
                    //            Message = $"{(short)HttpStatusCode.Forbidden} - {HttpStatusCode.Forbidden.ToString()} (A data de validade do Token expirou, 7 dias.)"
                    //        };

                    //        reply = request.CreateResponse(HttpStatusCode.Forbidden, tokenMsg);

                    //        log.Info($"{token.User} - {token.IP} - {request.RequestUri.ToString()} - {tokenMsg.Message}");

                    //        return await Task.FromResult(reply);
                    //    }

                    //   #endregion
                    //}

                    //Set The User Principal
                    //request.Properties.Add("MS_UserPrincipal", principal);
                    //HttpContext.Current.User = principal;
                    //Thread.CurrentPrincipal = principal;
                }
                catch (Exception ex)
                {
                    #region Erro no processamento do Token.

                    //string msgEx = (ex.Message.Length <= 100) ? ex.Message : string.Concat(ex.Message.Substring(0, 100), "...");

                    //tokenMsg = new TokenMessage
                    //{
                    //    Success = false,
                    //    Token = null,
                    //    Message = $"{(short)HttpStatusCode.BadRequest} - {HttpStatusCode.BadRequest.ToString()} (Erro no processamento do Token. [{msgEx}])"
                    //};

                    //reply = request.CreateResponse(HttpStatusCode.BadRequest, tokenMsg);

                    //log.Info($"{request.RequestUri.ToString()} - {tokenMsg.Message}");

                    //return await Task.FromResult(reply);

                    #endregion
                }
            }
            else
            {
                #region Token inexistente.

                //tokenMsg = new TokenMessage
                //{
                //    Success = false,
                //    Token = null,
                //    Message = $"{(short)HttpStatusCode.ExpectationFailed} - {HttpStatusCode.ExpectationFailed.ToString()} (Token inexistente.)"
                //};

                //reply = request.CreateResponse(HttpStatusCode.ExpectationFailed, tokenMsg);

                //log.Info($"{request.RequestUri.ToString()} - {tokenMsg.Message}");

                //return await Task.FromResult(reply);

                #endregion
            }

            //log.Info($"{request.RequestUri.ToString()} - {HttpStatusCode.OK.ToString()}");

            return await base.SendAsync(request, cancellationToken); ;
        }
    }

    public class PreflightRequestsHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Contains("Origin") && request.Method.Method.Equals("OPTIONS", StringComparison.InvariantCultureIgnoreCase))
            {
                var response = new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
                response.Headers.Add("Access-Control-Allow-Origin", "*");
                response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Token");
                response.Headers.Add("Access-Control-Allow-Methods", "GET, POST");

                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);

                return tsc.Task;
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}