using Microsoft.AspNetCore.Mvc;
using NetcareDoctorsAPI.BLL.BLLClasses;

namespace NetcareDoctorsAPI.Controllers
{
    [Route("api/Province")]
    [ApiController]
    public class ProvinceController : ControllerBase
    {
        private readonly ProvinceBLL ProvinceBLL;

        public ProvinceController()
        {
            ProvinceBLL = new();
        }

        [Route("V1/GetProvinces")]
        [HttpGet]
        public async Task<ActionResult> GetProvinces()
        {
            #region RequestValidation

            #endregion

            return Ok(await ProvinceBLL.GetProvinces());
        }
    }
}
