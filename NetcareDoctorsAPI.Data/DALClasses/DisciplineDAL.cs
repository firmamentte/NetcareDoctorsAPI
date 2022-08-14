using Microsoft.EntityFrameworkCore;
using NetcareDoctorsAPI.Data.Entities;

namespace NetcareDoctorsAPI.Data.DALClasses
{
    public class DisciplineDAL
    {
        public async Task<Discipline?> GetDisciplineByDisciplineName(NetcareDoctorsContext dbContext, string disciplineName)
        {
            return await (from discipline in dbContext.Disciplines
                          where discipline.DisciplineName == disciplineName
                          select discipline).
                          FirstOrDefaultAsync();
        }

        public async Task<List<Discipline>> GetDisciplines(NetcareDoctorsContext dbContext)
        {
            return await (from discipline in dbContext.Disciplines
                          select discipline).
                          ToListAsync();
        }
    }
}
