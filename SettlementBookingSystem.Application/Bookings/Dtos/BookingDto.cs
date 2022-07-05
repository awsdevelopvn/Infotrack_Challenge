using System;
using System.Text.Json.Serialization;

namespace SettlementBookingSystem.Application.Bookings.Dtos
{
    public class BookingDto
    {
        public BookingDto()
        {
            BookingId = Guid.NewGuid();
        }

        public Guid BookingId { get; }
        
        [JsonIgnore] public string Name { get; set; }
        [JsonIgnore] public string BookingTime { get; set; }
    }
}
