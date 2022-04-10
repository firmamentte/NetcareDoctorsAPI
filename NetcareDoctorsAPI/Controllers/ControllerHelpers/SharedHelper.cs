using Microsoft.Extensions.Primitives;

namespace NetcareDoctorsAPI.Controllers.ControllerHelpers
{
    public class SharedHelper
    {
        public string GetHeaderUsername(HttpRequest request)
        {
            if (request.Headers.TryGetValue("Username", out StringValues _username))
                return _username.FirstOrDefault();
            else
                return null;
        }
    }
}
