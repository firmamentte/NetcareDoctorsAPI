using Microsoft.AspNetCore.Mvc;
using NetcareDoctorsAPI.BLL.BLLClasses;
using NetcareDoctorsAPI.Filters;

namespace NetcareDoctorsAPI.Controllers
{
    [Route("api/Discipline")]
    [AuthenticateAccessToken]
    [ApiController]
    public class DisciplineController : ControllerBase
    {
        private readonly DisciplineBLL DisciplineBLL;

        public DisciplineController()
        {
            DisciplineBLL = new();
        }

        [Route("V1/GetDisciplines")]
        [HttpGet]
        public async Task<ActionResult> GetDisciplines()
        {
            #region RequestValidation

            #endregion

            return Ok(await DisciplineBLL.GetDisciplines());
        }
    }
}
