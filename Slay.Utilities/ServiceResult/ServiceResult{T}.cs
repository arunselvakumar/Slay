namespace Slay.Utilities.ServiceResult
{
    public sealed class ServiceResult<T> : ServiceResultBase
    {
        public T Value { get; set; }
    }
}