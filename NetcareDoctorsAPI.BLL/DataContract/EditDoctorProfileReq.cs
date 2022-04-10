namespace NetcareDoctorsAPI.BLL.DataContract
{
    public class EditDoctorProfileReq
    {
        public Guid DoctorProfileId { get; set; }
        public string IdNo { get; set; }
        public string TitleName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HpcsaNo { get; set; }
        public string DisciplineName { get; set; }
        public string ProvinceName { get; set; }
    }
}
