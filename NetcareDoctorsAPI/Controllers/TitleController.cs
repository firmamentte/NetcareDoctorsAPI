using Microsoft.AspNetCore.Mvc;
using NetcareDoctorsAPI.BLL.BLLClasses;
using NetcareDoctorsAPI.Filters;

namespace NetcareDoctorsAPI.Controllers
{
    [Route("api/Title")]
    [AuthenticateAccessToken]
    [ApiController]
    public class TitleController : ControllerBase
    {
        private readonly TitleBLL TitleBLL;
    
        public TitleController()
        {
            TitleBLL = new();
        }

        [Route("V1/GetTitles")]
        [HttpGet]
        public async Task<ActionResult> GetTitles()
        {
            #region RequestValidation

            #endregion

            return Ok(await TitleBLL.GetTitles());
        }
    }
}
