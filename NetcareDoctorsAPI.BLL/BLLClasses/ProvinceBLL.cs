using NetcareDoctorsAPI.BLL.DataContract;
using NetcareDoctorsAPI.Data.DALClasses;
using NetcareDoctorsAPI.Data.Entities;

namespace NetcareDoctorsAPI.BLL.BLLClasses
{
    public class ProvinceBLL
    {
        private readonly ProvinceDAL ProvinceDAL;

        public ProvinceBLL()
        {
            ProvinceDAL = new();
        }

        public async Task<List<ProvinceResp>> GetProvinces()
        {
            using NetcareDoctorsContext _dbContext = new();

            List<ProvinceResp> _provinceResps = new();

            foreach (var province in await ProvinceDAL.GetProvinces(_dbContext))
            {
                _provinceResps.Add(FillProvinceResp(province));
            }

            return _provinceResps;
        }

        private ProvinceResp FillProvinceResp(Province province)
        {
            return new ProvinceResp()
            {
                ProvinceName = province.ProvinceName
            };
        }
    }
}
