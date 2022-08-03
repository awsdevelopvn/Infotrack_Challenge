using System;
using SettlementBookingSystem.Application.Common;

namespace SettlementBookingSystem.Application.Entities;

public class LogEntry
{
    public string Id { get; set; }

    public bool IsActive { get; set; }
    public User Owner { get; set; }
    public User UpdatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }

    public LogLevel Level { get; set; }

    public string Domain { get; set; }

    public object Message { get; set; }

    public int? TimeElapsed { get; set; }
}