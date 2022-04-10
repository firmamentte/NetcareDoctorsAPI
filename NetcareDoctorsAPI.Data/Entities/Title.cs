using System;
using System.Collections.Generic;

namespace NetcareDoctorsAPI.Data.Entities
{
    public partial class Title
    {
        public Title()
        {
            DoctorProfiles = new HashSet<DoctorProfile>();
        }

        public Guid TitleId { get; set; }
        public string TitleName { get; set; } = null!;

        public virtual ICollection<DoctorProfile> DoctorProfiles { get; set; }
    }
}
