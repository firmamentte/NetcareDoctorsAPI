using Microsoft.Extensions.Logging;
using NetcareDoctorsAPI.BLL.DataContract;
using NetcareDoctorsAPI.Data;
using NetcareDoctorsAPI.Data.DALClasses;
using NetcareDoctorsAPI.Data.Entities;

namespace NetcareDoctorsAPI.BLL.BLLClasses
{
    public class DoctorProfileBLL : SharedBLL
    {

        private readonly ApplicationUserDAL ApplicationUserDAL;
        private readonly DoctorProfileDAL DoctorProfileDAL;
        private readonly TitleDAL TitleDAL;
        private readonly ProvinceDAL ProvinceDAL;
        private readonly DisciplineDAL DisciplineDAL;

        public DoctorProfileBLL()
        {
            ApplicationUserDAL = new();
            DoctorProfileDAL = new();
            TitleDAL = new();
            DisciplineDAL = new();
            ProvinceDAL = new();
        }

        public async Task<DoctorProfileResp> CreateDoctorProfile(string username, CreateDoctorProfileReq doctorProfileReq)
        {
            using NetcareDoctorsContext _dbContext = new();

            if (await DoctorProfileDAL.IsIdnoExisting(_dbContext, doctorProfileReq.IdNo))
            {
                RaiseServerError("ID No. already existing");
            }

            ApplicationUser? _creatorBy = await ApplicationUserDAL.GetApplicationUserByUsername(_dbContext, username),
                            _defaultUser = await ApplicationUserDAL.GetApplicationUserByUsername(_dbContext,
                            FirmamentUtilities.Utilities.GetEnumDescription(NetcareDoctorsAPIEnum.SpecialApplicationUsername.Default));

            Title? _title = await TitleDAL.GetTitleByTitleName(_dbContext, doctorProfileReq.TitleName);
            if (_title is null)
            {
                RaiseServerError("Invalid Title Name. The resource has been removed, had its name changed, or is unavailable.");
            }

            Discipline? _discipline = await DisciplineDAL.GetDisciplineByDisciplineName(_dbContext, doctorProfileReq.DisciplineName);
            if (_discipline is null)
            {
                RaiseServerError("Invalid Discipline Name. The resource has been removed, had its name changed, or is unavailable.");
            }

            Province? _province = await ProvinceDAL.GetProvinceByProvinceName(_dbContext, doctorProfileReq.ProvinceName);
            if (_province is null)
            {
                RaiseServerError("Invalid Province Name. The resource has been removed, had its name changed, or is unavailable.");
            }

            DoctorProfile _doctorProfile = new()
            {
                Firstname = doctorProfileReq.FirstName,
                Lastname = doctorProfileReq.LastName,
                Idno = doctorProfileReq.IdNo,
                Hpcsano = doctorProfileReq.HpcsaNo,
                Title = _title,
                Discipline = _discipline,
                Province = _province,
                CreatorNavigation = _creatorBy,
                LastModifierNavigation = _defaultUser,
                DeletedByNavigation = _defaultUser,
                CreationDate = DateTime.Now,
                LastModificationDate = FirmamentUtilities.Utilities.DateHelper.DefaultDate,
                DeletionDate = FirmamentUtilities.Utilities.DateHelper.DefaultDate
            };

            await _dbContext.AddAsync(_doctorProfile);
            await _dbContext.SaveChangesAsync();

            return FillDoctorProfileResp(_doctorProfile);
        }

        public async Task<DoctorProfileResp> EditDoctorProfile(string username, EditDoctorProfileReq editDoctorProfileReq)
        {
            using NetcareDoctorsContext _dbContext = new();

            DoctorProfile? _doctorProfile = await DoctorProfileDAL.GetDoctorProfileByDoctorProfileId(_dbContext, editDoctorProfileReq.DoctorProfileId);

            if (_doctorProfile is null)
            {
                RaiseServerError("Invalid Doctor Profile Id. The resource has been removed, had its name changed, or is unavailable.");
            }

            if (_doctorProfile?.Idno != editDoctorProfileReq.IdNo)
            {
                if (await DoctorProfileDAL.IsIdnoExisting(_dbContext, editDoctorProfileReq.IdNo))
                {
                    RaiseServerError("ID No. already existing");
                }
            }

            ApplicationUser? _creatorBy = await ApplicationUserDAL.GetApplicationUserByUsername(_dbContext, username);
            Title? _title = await TitleDAL.GetTitleByTitleName(_dbContext, editDoctorProfileReq.TitleName);
            Discipline? _discipline = await DisciplineDAL.GetDisciplineByDisciplineName(_dbContext, editDoctorProfileReq.DisciplineName);
            Province? _province = await ProvinceDAL.GetProvinceByProvinceName(_dbContext, editDoctorProfileReq.ProvinceName);

            _doctorProfile.Title = _title;
            _doctorProfile.Discipline = _discipline;
            _doctorProfile.Province = _province;
            _doctorProfile.Idno = editDoctorProfileReq.IdNo;
            _doctorProfile.Firstname = editDoctorProfileReq.FirstName;
            _doctorProfile.Lastname = editDoctorProfileReq.LastName;
            _doctorProfile.Hpcsano = editDoctorProfileReq.HpcsaNo;
            _doctorProfile.LastModifierNavigation = _creatorBy;
            _doctorProfile.LastModificationDate = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            return FillDoctorProfileResp(_doctorProfile);
        }

        public async Task DeleteDoctorProfile(string username, Guid doctorProfileId)
        {
            using NetcareDoctorsContext _dbContext = new();

            DoctorProfile? _doctorProfile = await DoctorProfileDAL.GetDoctorProfileByDoctorProfileId(_dbContext, doctorProfileId);

            if (_doctorProfile is null)
            {
                RaiseServerError("Invalid Doctor Profile Id. The resource has been removed, had its name changed, or is unavailable.");
            }

            ApplicationUser? _deletedBy = await ApplicationUserDAL.GetApplicationUserByUsername(_dbContext, username);

            _doctorProfile.DeletedByNavigation = _deletedBy;
            _doctorProfile.DeletionDate = DateTime.Now;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<DoctorProfileResp> GetDoctorProfileByDoctorProfileId(Guid doctorProfileId)
        {
            using NetcareDoctorsContext _dbContext = new();

            return FillDoctorProfileResp(await DoctorProfileDAL.GetDoctorProfileByDoctorProfileId(_dbContext, doctorProfileId));
        }

        public async Task<List<DoctorProfileResp>> GetDoctorProfileByCriteria
        (string? idNo, string? titleName, string? firstname, string? lastname, string? hpcsaNo, string? disciplineName, string? provinceName)
        {
            using NetcareDoctorsContext _dbContext = new();

            List<DoctorProfileResp> _doctorProfileResps = new();

            foreach (var doctorProfile in await DoctorProfileDAL.GetDoctorProfileByCriteria
                                          (_dbContext, idNo, titleName, firstname, lastname, hpcsaNo, disciplineName, provinceName))
            {
                _doctorProfileResps.Add(FillDoctorProfileResp(doctorProfile));
            }

            return _doctorProfileResps;
        }

        private DoctorProfileResp FillDoctorProfileResp(DoctorProfile doctorProfile)
        {
            return new DoctorProfileResp()
            {
                DisciplineName = doctorProfile.DisciplineName,
                ProvinceName = doctorProfile.ProvinceName,
                TitleName = doctorProfile.TitleName,
                FirstName = doctorProfile.Firstname,
                LastName = doctorProfile.Lastname,
                HpcsaNo = doctorProfile.Hpcsano,
                IdNo = doctorProfile.Idno,
                DoctorProfileId = doctorProfile.DoctorProfileId
            };
        }
    }
}
