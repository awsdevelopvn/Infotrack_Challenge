namespace Varoom.Admin.Service.Features.User.Models
{
    public class BambooHrUser
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PreferredName { get; set; }
        public string JobTitle { get; set; }
        public string WorkPhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkEmail { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Division { get; set; }
        public string LinkedIn { get; set; }
        public string Pronouns { get; set; }
        public string WorkPhoneExtension { get; set; }
        public string Supervisor { get; set; }
        public bool PhotoUploaded { get; set; }
        public string PhotoUrl { get; set; }
        public int CanUploadPhoto { get; set; }
    }
}
