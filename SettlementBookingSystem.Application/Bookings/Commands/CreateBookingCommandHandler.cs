using MediatR;
using SettlementBookingSystem.Application.Bookings.Dtos;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using SettlementBookingSystem.Application.Behaviours;
using SettlementBookingSystem.Application.Exceptions;

namespace SettlementBookingSystem.Application.Bookings.Commands
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingDto>
    {
        public CreateBookingCommandHandler()
        {
        }

        public async Task<BookingDto> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            if (Exists(request.BookingTime))
            {
                throw new ConflictException($"The booking time {request.BookingTime} is already reserved");
            }

            var bookingTimeObj = request.BookingTime.GetBookTime();
            if (bookingTimeObj.hour > 24 || bookingTimeObj.minute > 59 || bookingTimeObj.hour < 0 || bookingTimeObj.minute < 0)
            {
                throw new ValidationException("The booking time is an invalid time. The time should be a 24-hour time (00:00 - 23:59)");
            }

            if (bookingTimeObj.hour is < 9 or > 16)
            {
                throw new ValidationException("The booking time should be in range 9am to 4pm");
            }

            // TODO: If there are more fields ...it should be use the mapper tool (such as automapper or mapster)
            var newBooking = new BookingDto {Name = request.Name, BookingTime = request.BookingTime};

            MemoryDatabase.Bookings.Add(newBooking.BookingId, newBooking);
            return newBooking;
        }

        private static bool Exists(string bookingTime)
        {
            return (from bookingDto in MemoryDatabase.Bookings
                let bookingTimeObj = bookingTime.GetBookTime()
                let existingBookingTimeObj = bookingDto.Value.BookingTime.GetBookTime()
                where bookingDto.Value.BookingTime == bookingTime || bookingTimeObj.hour == existingBookingTimeObj.hour
                select bookingDto).Any();
        }
    }
}
