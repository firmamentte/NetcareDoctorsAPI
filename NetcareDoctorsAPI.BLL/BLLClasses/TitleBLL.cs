using NetcareDoctorsAPI.BLL.DataContract;
using NetcareDoctorsAPI.Data.DALClasses;
using NetcareDoctorsAPI.Data.Entities;

namespace NetcareDoctorsAPI.BLL.BLLClasses
{
    public class TitleBLL
    {
        private readonly TitleDAL TitleDAL;

        public TitleBLL()
        {
            TitleDAL = new();
        }

        public async Task<List<TitleResp>> GetTitles()
        {
            using NetcareDoctorsContext _dbContext = new();

            List<TitleResp> _titleResps = new();

            foreach (var title in await TitleDAL.GetTitles(_dbContext))
            {
                _titleResps.Add(FillTitleResp(title));
            }

            return _titleResps;
        }

        private TitleResp FillTitleResp(Title title)
        {
            return new TitleResp()
            {
                TitleName = title.TitleName
            };
        }
    }
}
