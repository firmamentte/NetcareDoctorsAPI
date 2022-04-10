using System;
using System.Collections.Generic;

namespace NetcareDoctorsAPI.Data.Entities
{
    public partial class ApplicationUser
    {
        public ApplicationUser()
        {
            DoctorProfileCreatorNavigations = new HashSet<DoctorProfile>();
            DoctorProfileDeletedByNavigations = new HashSet<DoctorProfile>();
            DoctorProfileLastModifierNavigations = new HashSet<DoctorProfile>();
        }

        public Guid ApplicationUserId { get; set; }
        public string Username { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public string ApplicationUserType { get; set; } = null!;

        public virtual ICollection<DoctorProfile> DoctorProfileCreatorNavigations { get; set; }
        public virtual ICollection<DoctorProfile> DoctorProfileDeletedByNavigations { get; set; }
        public virtual ICollection<DoctorProfile> DoctorProfileLastModifierNavigations { get; set; }
    }
}
