using System;
using System.Collections.Generic;

namespace SettlementBookingSystem.Application.Entities
{
    public class LogTraceEntity: IEntity
    {
        public IEnumerable<LogEntry> Message { get; set; }
        public double ProcessingMilliseconds { get; set; }
        public string Id { get; set; }
        public bool IsActive { get; set; }
        public User CreatedBy { get; set; }
        public User UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}