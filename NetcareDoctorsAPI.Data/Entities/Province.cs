using System;
using System.Collections.Generic;

namespace NetcareDoctorsAPI.Data.Entities
{
    public partial class Province
    {
        public Province()
        {
            DoctorProfiles = new HashSet<DoctorProfile>();
        }

        public Guid ProvinceId { get; set; }
        public string ProvinceName { get; set; } = null!;

        public virtual ICollection<DoctorProfile> DoctorProfiles { get; set; }
    }
}
