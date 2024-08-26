namespace Ordering.Domain.ValueObjects
{
    public record OrderName
    {
        public string Value { get; }
        private OrderName(string value) => Value = value;
        public static OrderName Of(string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException("OrderName cannot be empty.");
            }

            return new OrderName(value);
        }
    }
}
