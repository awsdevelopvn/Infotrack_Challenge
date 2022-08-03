using System.Collections.Generic;
using Varoom.Admin.Service.Features.User.Models;

namespace SettlementBookingSystem.Application.Features.Users.Models
{
    public class ImportBambooHrUserResponse
    {
        public IEnumerable<BambooHrField> Fields { get; set; }
        public IEnumerable<BambooHrUser> Employees { get; set; }
    }

    public class BambooHrField
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }
}
