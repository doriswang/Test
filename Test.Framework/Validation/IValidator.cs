namespace Test.Framework.Validation
{
    public interface IValidator
    {
        bool IsValid { get; }
        string Message { get; }
        void Validate();
    }
}
