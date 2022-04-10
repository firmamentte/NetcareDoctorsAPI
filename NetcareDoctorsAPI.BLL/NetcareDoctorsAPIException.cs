
namespace NetcareDoctorsAPI.BLL
{
    public class NetcareDoctorsAPIException : Exception
    {
        public NetcareDoctorsAPIException(string errorMessage) : base(errorMessage)
        { }
    }
}
