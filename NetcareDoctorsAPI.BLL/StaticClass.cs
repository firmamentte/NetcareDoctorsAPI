using Microsoft.Extensions.Configuration;
using static NetcareDoctorsAPI.Data.StaticClass;

namespace NetcareDoctorsAPI.BLL
{
    public static class StaticClass
    {
        public static void InitializeApplicationSettings(IConfiguration configuration)
        {
            DatabaseHelper.ConnectionString ??= configuration["ConnectionStrings:DatabasePath"];
        }
    }
}