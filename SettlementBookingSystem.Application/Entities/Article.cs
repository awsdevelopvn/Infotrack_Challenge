using System;

namespace SettlementBookingSystem.Application.Entities;

public class Article: IEntity
{
    public string Id { get; set; }
    public string Question { get; set; }
    public DateTime? Date { get; set; }
    public bool DidAnswer { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string Description { get; set; }
    
    public bool IsActive { get; set; }
    public User CreatedBy { get; set; }
    public User UpdatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
}