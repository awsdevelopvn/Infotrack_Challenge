namespace Varoom.Admin.Service.Features.User.Models;

public class AddUserRequest
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Fullname { get; set; }
    public string Department { get; set; }
    public string DepartmentId { get; set; }
    public string Division { get; set; }
    public string CompanyId { get; set; }
    public string CompanyDomain { get; set; }
    public string Supervisor { get; set; }
    public string SupervisorId { get; set; }
    public string SupervisorEmail { get; set; }
    public string MobilePhone { get; set; }
    public string Position { get; set; }
    public string ImageURL { get; set; }
    public string EmployeeNumber { get; set; }
    public string EmployeeId { get; set; }
}