using Microsoft.EntityFrameworkCore;
using NetcareDoctorsAPI.Data.Entities;

namespace NetcareDoctorsAPI.Data.DALClasses
{
    public class TitleDAL
    {
        public async Task<Title?> GetTitleByTitleName(NetcareDoctorsContext dbContext, string titleName)
        {
            return await (from title in dbContext.Titles
                          where title.TitleName == titleName
                          select title).
                          FirstOrDefaultAsync();
        }

        public async Task<List<Title>> GetTitles(NetcareDoctorsContext dbContext)
        {
            return await (from title in dbContext.Titles
                          select title).
                          ToListAsync();
        }
    }
}
