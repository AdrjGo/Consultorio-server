namespace Domain.ValueObjects
{
    public class FilePath : ValueObject
    {
        public string Value { get; }
        private FilePath() { }

        public FilePath(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El path no puede estar vacío.");

            if (value.IndexOfAny(System.IO.Path.GetInvalidPathChars()) >= 0)
                throw new ArgumentException("Path no válido.");

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}