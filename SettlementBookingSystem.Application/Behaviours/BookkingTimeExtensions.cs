using FluentValidation;

namespace SettlementBookingSystem.Application.Behaviours
{
    public static class BookingTimeExtensions
    {
        /// <summary>
        /// Get hour and minute of booking time
        /// </summary>
        /// <param name="bookingTime"></param>
        /// <returns></returns>
        /// <exception cref="ValidationException"></exception>
        public static (int hour, int minute) GetBookTime(this string bookingTime)
        {
            if (!bookingTime.Contains(':'))
            {
                throw new ValidationException("Invalid format booking time");
            }

            var timeParts = bookingTime.Split(':');
            if (timeParts.Length != 2)
            {
                throw new ValidationException("Invalid format booking time");
            }
            
            return (int.Parse(timeParts[0]), int.Parse(timeParts[1]));
        }
    }
}