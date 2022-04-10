using System;
using System.Collections.Generic;

namespace NetcareDoctorsAPI.Data.Entities
{
    public partial class DoctorProfile
    {
        public virtual string TitleName
        {
            get
            {
                return Title.TitleName;
            }
        }

        public virtual string ProvinceName
        {
            get
            {
                return Province.ProvinceName;
            }
        }

        public virtual string DisciplineName
        {
            get
            {
                return Discipline.DisciplineName;
            }
        }
    }
}
