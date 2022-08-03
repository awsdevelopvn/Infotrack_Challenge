using System;

namespace SettlementBookingSystem.Application.Entities;

public interface IEntity
{
    string Id { get; set; }
    bool IsActive { get; set; }
    User CreatedBy { get; set; }
    User UpdatedBy { get; set; }
    DateTime CreatedOn { get; set; }
    DateTime? UpdatedOn { get; set; }
}