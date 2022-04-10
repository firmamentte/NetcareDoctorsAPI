using Microsoft.EntityFrameworkCore;
using NetcareDoctorsAPI.Data.Entities;

namespace NetcareDoctorsAPI.Data.DALClasses
{
    public class ProvinceDAL
    {
        public async Task<Province?> GetProvinceByProvinceName(NetcareDoctorsContext dbContext, string provinceName)
        {
            return await (from province in dbContext.Provinces
                          where province.ProvinceName == provinceName
                          select province).
                          FirstOrDefaultAsync();
        }

        public async Task<List<Province>> GetProvinces(NetcareDoctorsContext dbContext)
        {
            return await (from province in dbContext.Provinces
                          select province).
                          ToListAsync();
        }
    }
}
