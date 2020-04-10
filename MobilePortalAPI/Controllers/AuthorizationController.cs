using Business;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace MobilePortalAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController :ControllerBase
    {
        private Business.IAuthorizationService _authorizationService;

        public AuthorizationController(Business.IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateDTO model)
        {
            var result = _authorizationService.Authenticate(model.CommonServerName, model.Username, model.Password);
            return new OkObjectResult(result);
        }
    }
}
