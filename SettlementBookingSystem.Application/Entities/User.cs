using System;

namespace SettlementBookingSystem.Application.Entities;

public class User: IEntity
{
    public string Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public User CreatedBy { get; set; }
    public User UpdatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
}