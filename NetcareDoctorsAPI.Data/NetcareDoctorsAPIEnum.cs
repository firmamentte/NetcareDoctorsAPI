using System.ComponentModel;

namespace NetcareDoctorsAPI.Data
{
    public class NetcareDoctorsAPIEnum
    {
        public enum SpecialApplicationUsername
        {
            [Description("Default")]
            Default
        }

        public enum ApplicationUserType
        {
            [Description("Administrator")]
            Administrator,
            [Description("GeneralUser")]
            GeneralUser
        }
    }
}
