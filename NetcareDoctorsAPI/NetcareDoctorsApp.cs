using NetcareDoctorsAPI.BLL;

namespace NetcareDoctorsAPI
{
    public static class NetcareDoctorsApp
    {  
        public static void InitializeApplicationSettings(this WebApplication app)
        {
            StaticClass.InitializeApplicationSettings(app.Configuration);
        }
    }
}
 