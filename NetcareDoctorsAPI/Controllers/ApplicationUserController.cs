using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using NetcareDoctorsAPI.BLL.BLLClasses;
using NetcareDoctorsAPI.BLL.DataContract;
using NetcareDoctorsAPI.Filters;
using System.Net;

namespace NetcareDoctorsAPI.Controllers
{
    [Route("api/ApplicationUser")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly ApplicationUserBLL ApplicationUserBLL;
        public ApplicationUserController()
        {
            ApplicationUserBLL = new();
        }

        [Route("V1/Authenticate")]
        [HttpPost]
        public async Task<ActionResult> Authenticate()
        {
            #region RequestValidation

            #endregion

            AuthenticateResp _authenticateResp = await ApplicationUserBLL.Authenticate();

            Response.Headers.Add("AccessToken", _authenticateResp.AccessToken);
            Response.Headers.Add("AccessTokenExpiryDate", _authenticateResp.ExpiryDate.ToString());

            return Ok();
        }

        [Route("V1/SignUp")]
        [AuthenticateAccessToken]
        [HttpPost]
        public async Task<ActionResult> SignUp()
        {
            #region RequestValidation

            ModelState.Clear();

            if (!Request.Headers.TryGetValue("Username", out StringValues _username))
            {
                ModelState.AddModelError("Username", "Username required");
            }

            if (!Request.Headers.TryGetValue("UserPassword", out StringValues _userPassword))
            {
                ModelState.AddModelError("UserPassword", "User Password required");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiErrorResp(ModelState));
            }

            #endregion

            await ApplicationUserBLL.SignUp(_username.FirstOrDefault(), _userPassword.FirstOrDefault());

            return StatusCode((int)HttpStatusCode.Created);
        }

        [Route("V1/SignIn")]
        [AuthenticateAccessToken]
        [HttpPut]
        public async Task<ActionResult> SignIn()
        {
            #region RequestValidation

            ModelState.Clear();

            if (!Request.Headers.TryGetValue("Username", out StringValues _username))
            {
                ModelState.AddModelError("Username", "Username required");
            }

            if (!Request.Headers.TryGetValue("UserPassword", out StringValues _userPassword))
            {
                ModelState.AddModelError("UserPassword", "User Password required");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiErrorResp(ModelState));
            }

            #endregion

            SignInResp _signInResp = await ApplicationUserBLL.SignIn(_username.FirstOrDefault(), _userPassword.FirstOrDefault());

            Response.Headers.Add("Username", _signInResp.Username);
            Response.Headers.Add("ApplicationUserType", _signInResp.ApplicationUserType);

            return Ok();
        }
    }
}
