namespace Varoom.Admin.Service.Features.User.Models
{
    public class ImportUserRequest
    {
        public string ApiKey { get; set; }
        public string Domain { get; set; } = "dfo";
        public string CompanyId { get; set; }
    }
}
