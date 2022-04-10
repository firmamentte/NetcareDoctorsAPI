using Microsoft.EntityFrameworkCore;
using NetcareDoctorsAPI.Data.Entities;

namespace NetcareDoctorsAPI.Data.DALClasses
{
    public class DoctorProfileDAL
    {
        public async Task<List<DoctorProfile>> GetDoctorProfileByCriteria
        (NetcareDoctorsContext dbContext, string? idNo, string? titleName, string? firstname,
        string? lastname, string? hpcsaNo, string? disciplineName, string? provinceName)
        {
            var _queryable = from doctorProfile in dbContext.DoctorProfiles
                             join title in dbContext.Titles
                             on doctorProfile.TitleId equals title.TitleId
                             join province in dbContext.Provinces
                             on doctorProfile.ProvinceId equals province.ProvinceId
                             join discipline in dbContext.Disciplines
                             on doctorProfile.DisciplineId equals discipline.DisciplineId
                             where doctorProfile.DeletionDate == FirmamentUtilities.Utilities.DateHelper.DefaultDate
                             select new
                             {
                                 doctorProfile,
                                 title,
                                 province,
                                 discipline
                             };

            if (!string.IsNullOrWhiteSpace(idNo))
            {
                _queryable = _queryable.Where(x => x.doctorProfile.Idno.Contains(idNo));
            }

            if (!string.IsNullOrWhiteSpace(firstname))
            {
                _queryable = _queryable.Where(x => x.doctorProfile.Firstname.Contains(firstname));
            }

            if (!string.IsNullOrWhiteSpace(lastname))
            {
                _queryable = _queryable.Where(x => x.doctorProfile.Lastname.Contains(lastname));
            }

            if (!string.IsNullOrWhiteSpace(hpcsaNo))
            {
                _queryable = _queryable.Where(x => x.doctorProfile.Hpcsano.Contains(hpcsaNo));
            }

            if (!string.IsNullOrWhiteSpace(titleName))
            {
                _queryable = _queryable.Where(x => x.title.TitleName.Contains(titleName));
            }

            if (!string.IsNullOrWhiteSpace(disciplineName))
            {
                _queryable = _queryable.Where(x => x.discipline.DisciplineName.Contains(disciplineName));
            }

            if (!string.IsNullOrWhiteSpace(provinceName))
            {
                _queryable = _queryable.Where(x => x.province.ProvinceName.Contains(provinceName));
            }

            return await _queryable.Select(x => x.doctorProfile).ToListAsync();
        }

        public async Task<DoctorProfile?> GetDoctorProfileByDoctorProfileId(NetcareDoctorsContext dbContext, Guid doctorProfileId)
        {
            return await (from doctorProfile in dbContext.DoctorProfiles
                          where doctorProfile.DoctorProfileId == doctorProfileId
                          select doctorProfile).
                          FirstOrDefaultAsync();
        }

        public async Task<bool> IsIdnoExisting(NetcareDoctorsContext dbContext, string Idno)
        {
            return await (from doctorProfile in dbContext.DoctorProfiles
                          where doctorProfile.Idno == Idno &&
                                doctorProfile.DeletionDate == FirmamentUtilities.Utilities.DateHelper.DefaultDate
                          select doctorProfile).
                          AnyAsync();
        }
    } 
}
