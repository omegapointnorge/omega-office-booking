using System.ComponentModel.DataAnnotations;

namespace server.Models.DTOs.Request
{
    public class CreateBookingRequest
    {
        public int SeatId { get; set; }

        public DateTime BookingDateTime { get; set; }

        [MustNotBeNullWhenIsEventIsTrue(ErrorMessage = "SeatList must not be null when IsEvent is true.")]
        [MustContainItemsWhenIsEventIsTrue(ErrorMessage = "SeatList must contain at least one item when IsEvent is true.")]
        public List<int>? SeatList { get; set; }

        public bool? IsEvent { get; set; } = false;
        public string? reCAPTCHAToken { get; set; }

    }

    public class CreateEventRequest
    {
        public string? EventName { get; set; }
        public DateTime BookingDateTime { get; set; }
        public List<int>? SeatList { get; set; }
    }

    internal class MustNotBeNullWhenIsEventIsTrueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var isEventProperty = validationContext.ObjectType.GetProperty("IsEvent");
            var isEventValue = isEventProperty?.GetValue(validationContext.ObjectInstance) as bool?;

            if (isEventValue.HasValue && isEventValue.Value && value == null)
            {
                return new ValidationResult(ErrorMessage);

            }
            return ValidationResult.Success;

        }
    }

    internal class MustContainItemsWhenIsEventIsTrueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var isEventProperty = validationContext.ObjectType.GetProperty("IsEvent");
            var isEventValue = isEventProperty?.GetValue(validationContext.ObjectInstance) as bool?;

            if (isEventValue.HasValue && isEventValue.Value && (value == null || !((IEnumerable<int>)value).Any()))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}