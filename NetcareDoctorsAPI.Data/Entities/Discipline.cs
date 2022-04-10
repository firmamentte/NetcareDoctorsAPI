using System;
using System.Collections.Generic;

namespace NetcareDoctorsAPI.Data.Entities
{
    public partial class Discipline
    {
        public Discipline()
        {
            DoctorProfiles = new HashSet<DoctorProfile>();
        }

        public Guid DisciplineId { get; set; }
        public string DisciplineName { get; set; } = null!;

        public virtual ICollection<DoctorProfile> DoctorProfiles { get; set; }
    }
}
