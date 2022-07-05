using System;
using System.Collections.Generic;
using SettlementBookingSystem.Application.Bookings.Dtos;

namespace SettlementBookingSystem.Application
{
    public static class MemoryDatabase
    {
        public static IDictionary<Guid, BookingDto> Bookings = new Dictionary<Guid, BookingDto>();
    }
}