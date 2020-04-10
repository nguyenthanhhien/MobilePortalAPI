using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Business;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MobilePortalAPI.Helpers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IAuthorizationService _userService;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IAuthorizationService userService)
            : base(options, logger, encoder, clock)
        {
            _userService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");
            var isLogin = -1;
            try
            {
                var test = Request.Headers["Authorization"];
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 3);
                var username = credentials[0];
                var password = credentials[1];
                //var commonServerName = "HCM-QA-T430,1441\\AU2";
                var commonServerName = credentials[2];
                isLogin = _userService.Authenticate(commonServerName, username, password);
                if (isLogin == (int)LoginStatus.Fail)
                    return AuthenticateResult.Fail("Invalid Username or Password");
                if (isLogin == (int)LoginStatus.NoPermission)
                    return AuthenticateResult.Fail("You not allow to access dealer data");
                var claims = new[] {
                    new Claim(Business.ClaimTypes.Username.ToString(), username),
                    new Claim(Business.ClaimTypes.Password.ToString(), password),
                    new Claim(Business.ClaimTypes.CommonServerName.ToString(), commonServerName),
                    new Claim(System.Security.Claims.ClaimTypes.NameIdentifier, username),
                    new Claim(System.Security.Claims.ClaimTypes.Name, username)
                };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }


        }
    }
}
