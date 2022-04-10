using NetcareDoctorsAPI.BLL.DataContract;
using NetcareDoctorsAPI.Data.DALClasses;
using NetcareDoctorsAPI.Data.Entities;

namespace NetcareDoctorsAPI.BLL.BLLClasses
{
    public class DisciplineBLL
    {
        private readonly DisciplineDAL DisciplineDAL;

        public DisciplineBLL()
        {
            DisciplineDAL = new();
        }

        public async Task<List<DisciplineResp>> GetDisciplines()
        {
            using NetcareDoctorsContext _dbContext = new();

            List<DisciplineResp> _disciplineResps = new();

            foreach (var discipline in await DisciplineDAL.GetDisciplines(_dbContext))
            {
                _disciplineResps.Add(FillDisciplineResp(discipline));
            }

            return _disciplineResps;
        }

        private DisciplineResp FillDisciplineResp(Discipline discipline)
        {
            return new DisciplineResp()
            {
                DisciplineName = discipline.DisciplineName
            };
        }
    }
}
