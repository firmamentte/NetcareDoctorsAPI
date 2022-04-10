using System;
using System.Collections.Generic;

namespace NetcareDoctorsAPI.Data.Entities
{
    public partial class DoctorProfile
    {
        public Guid DoctorProfileId { get; set; }
        public Guid TitleId { get; set; }
        public Guid DisciplineId { get; set; }
        public Guid ProvinceId { get; set; }
        public string Idno { get; set; } = null!;
        public string Hpcsano { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public Guid Creator { get; set; }
        public Guid LastModifier { get; set; }
        public Guid DeletedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }
        public DateTime DeletionDate { get; set; }

        public virtual ApplicationUser CreatorNavigation { get; set; } = null!;
        public virtual ApplicationUser DeletedByNavigation { get; set; } = null!;
        public virtual Discipline Discipline { get; set; } = null!;
        public virtual ApplicationUser LastModifierNavigation { get; set; } = null!;
        public virtual Province Province { get; set; } = null!;
        public virtual Title Title { get; set; } = null!;
    }
}
