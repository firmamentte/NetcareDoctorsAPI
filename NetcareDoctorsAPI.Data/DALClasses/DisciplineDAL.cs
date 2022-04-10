using Microsoft.EntityFrameworkCore;
using NetcareDoctorsAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
