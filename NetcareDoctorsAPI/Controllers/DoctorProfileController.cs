using Microsoft.AspNetCore.Mvc;
using NetcareDoctorsAPI.BLL.BLLClasses;
using NetcareDoctorsAPI.BLL.DataContract;
using NetcareDoctorsAPI.Controllers.ControllerHelpers;
using NetcareDoctorsAPI.Filters;

namespace NetcareDoctorsAPI.Controllers
{
    [Route("api/DoctorProfile")]
    [AuthenticateAccessToken]
    [ApiController]
    public class DoctorProfileController : ControllerBase
    {
        private readonly DoctorProfileBLL DoctorProfileBLL;
        private readonly SharedHelper SharedHelper;
        public DoctorProfileController()
        {
            DoctorProfileBLL = new();
            SharedHelper = new();
        }

        [Route("V1/CreateDoctorProfile")]
        [HttpPost]
        [AuthenticateAdministratorApplicationUser]
        public async Task<ActionResult> CreateDoctorProfile([FromBody] CreateDoctorProfileReq createDoctorProfileReq)
        {

            #region RequestValidation

            ModelState.Clear();

            if (createDoctorProfileReq is null)
            {
                ModelState.AddModelError("CreateDoctorProfileReq", "Create Doctor Profile request can not be null");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(createDoctorProfileReq.IdNo))
                {
                    ModelState.AddModelError("IdNo", "ID No. required");
                }
                else
                {
                    if (!FirmamentUtilities.Utilities.ValidationHelper.IsSaIdValid(createDoctorProfileReq.IdNo))
                    {
                        ModelState.AddModelError("IdNoInvalid", "Invalid ID No.");
                    }
                }

                if (string.IsNullOrWhiteSpace(createDoctorProfileReq.HpcsaNo))
                {
                    ModelState.AddModelError("Hpcsano", "Hpcsa No. required");
                }

                if (string.IsNullOrWhiteSpace(createDoctorProfileReq.TitleName))
                {
                    ModelState.AddModelError("TitleName", "Title Name required");
                }

                if (string.IsNullOrWhiteSpace(createDoctorProfileReq.FirstName))
                {
                    ModelState.AddModelError("FirstName", "First Name required");
                }

                if (string.IsNullOrWhiteSpace(createDoctorProfileReq.LastName))
                {
                    ModelState.AddModelError("LastName", "Last Name required");
                }

                if (string.IsNullOrWhiteSpace(createDoctorProfileReq.DisciplineName))
                {
                    ModelState.AddModelError("DisciplineName", "Discipline Name required");
                }

                if (string.IsNullOrWhiteSpace(createDoctorProfileReq.ProvinceName))
                {
                    ModelState.AddModelError("ProvinceName", "Province Name required");
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiErrorResp(ModelState));
            }

            #endregion

            return Created(string.Empty, await DoctorProfileBLL.CreateDoctorProfile(SharedHelper.GetHeaderUsername(Request), createDoctorProfileReq));
        }

        [Route("V1/EditDoctorProfile")]
        [HttpPut]
        [AuthenticateAdministratorApplicationUser]
        public async Task<ActionResult> EditDoctorProfile([FromBody] EditDoctorProfileReq editDoctorProfileReq)
        {

            #region RequestValidation

            ModelState.Clear();

            if (editDoctorProfileReq is null)
            {
                ModelState.AddModelError("EditDoctorProfileReq", "Edit Doctor Profile request can not be null");
            }
            else
            {
                if (editDoctorProfileReq.DoctorProfileId == Guid.Empty)
                {
                    ModelState.AddModelError("DoctorProfileId", "Doctor Profile Id must be a globally unique identifier and not empty");
                }

                if (string.IsNullOrWhiteSpace(editDoctorProfileReq.IdNo))
                {
                    ModelState.AddModelError("IdNo", "ID No. required");
                }
                else
                {
                    if (!FirmamentUtilities.Utilities.ValidationHelper.IsSaIdValid(editDoctorProfileReq.IdNo))
                    {
                        ModelState.AddModelError("IdNoInvalid", "Invalid ID No.");
                    }
                }

                if (string.IsNullOrWhiteSpace(editDoctorProfileReq.HpcsaNo))
                {
                    ModelState.AddModelError("Hpcsano", "Hpcsa No. required");
                }

                if (string.IsNullOrWhiteSpace(editDoctorProfileReq.TitleName))
                {
                    ModelState.AddModelError("TitleName", "Title Name required");
                }

                if (string.IsNullOrWhiteSpace(editDoctorProfileReq.FirstName))
                {
                    ModelState.AddModelError("FirstName", "First Name required");
                }

                if (string.IsNullOrWhiteSpace(editDoctorProfileReq.LastName))
                {
                    ModelState.AddModelError("LastName", "Last Name required");
                }

                if (string.IsNullOrWhiteSpace(editDoctorProfileReq.DisciplineName))
                {
                    ModelState.AddModelError("DisciplineName", "Discipline Name required");
                }

                if (string.IsNullOrWhiteSpace(editDoctorProfileReq.ProvinceName))
                {
                    ModelState.AddModelError("ProvinceName", "Province Name required");
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiErrorResp(ModelState));
            }

            #endregion

            return Ok(await DoctorProfileBLL.EditDoctorProfile(SharedHelper.GetHeaderUsername(Request), editDoctorProfileReq));
        }

        [Route("V1/DeleteDoctorProfile")]
        [HttpDelete]
        [AuthenticateAdministratorApplicationUser]
        public async Task<ActionResult> DeleteDoctorProfile([FromQuery] Guid doctorProfileId)
        {
            #region RequestValidation

            ModelState.Clear();

            if (doctorProfileId == Guid.Empty)
            {
                ModelState.AddModelError("DoctorProfileId", "Doctor Profile Id must be a globally unique identifier and not empty");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiErrorResp(ModelState));
            }

            #endregion

            await DoctorProfileBLL.DeleteDoctorProfile(SharedHelper.GetHeaderUsername(Request), doctorProfileId);

            return Ok();
        }

        [Route("V1/GetDoctorProfileByDoctorProfileId")]
        [HttpGet]
        public async Task<ActionResult> GetDoctorProfileByDoctorProfileId([FromQuery] Guid doctorProfileId)
        {
            #region RequestValidation

            ModelState.Clear();

            if (doctorProfileId == Guid.Empty)
            {
                ModelState.AddModelError("DoctorProfileId", "Doctor Profile Id must be a globally unique identifier and not empty");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiErrorResp(ModelState));
            }

            #endregion

            return Ok(await DoctorProfileBLL.GetDoctorProfileByDoctorProfileId(doctorProfileId));
        }

        [Route("V1/GetDoctorProfileByCriteria")]
        [HttpGet]
        public async Task<ActionResult> GetDoctorProfileByCriteria(
            [FromQuery]  string? idNo, 
            [FromQuery]  string? titleName, 
            [FromQuery]  string? firstname, 
            [FromQuery]  string? lastname, 
            [FromQuery]  string? hpcsaNo, 
            [FromQuery]  string? disciplineName, 
            [FromQuery]  string? provinceName)
        {
            #region RequestValidation

            ModelState.Clear();

            if (string.IsNullOrWhiteSpace(idNo) && string.IsNullOrWhiteSpace(titleName) &&
                string.IsNullOrWhiteSpace(firstname) && string.IsNullOrWhiteSpace(lastname) && string.IsNullOrWhiteSpace(hpcsaNo) &&
                string.IsNullOrWhiteSpace(disciplineName) && string.IsNullOrWhiteSpace(provinceName))
            {
                ModelState.AddModelError("EmptyRequest", "At least one parameter must be supplied");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiErrorResp(ModelState));
            }

            #endregion

            return Ok(await DoctorProfileBLL.GetDoctorProfileByCriteria(idNo, titleName, firstname, lastname, hpcsaNo, disciplineName, provinceName));
        }
    }
}
