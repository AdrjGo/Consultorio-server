using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public sealed class PhoneNumber : ValueObject
    {
        public string Value { get; }

        public PhoneNumber(string value)
        {
            Value = value;
        }

        protected PhoneNumber() { }

        public static PhoneNumber Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El número de teléfono no puede estar vacío.");

            if (!Regex.IsMatch(value, @"^\d{10}$"))
                throw new FormatException("El formato del número de teléfono es inválido.");

            return new PhoneNumber(value.Trim());
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}