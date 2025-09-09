namespace Domain.ValueObjects
{
    public class Url : ValueObject
    {
        public string Value { get; }

        private Url() { }

        public Url(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El URL no puede estar vacío.");

            if (!Uri.TryCreate(value, UriKind.Absolute, out var uriResult) ||
                (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
            {
                throw new ArgumentException("El formato del URL es inválido.");
            }

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}
