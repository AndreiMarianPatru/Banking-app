namespace PaymentGateway.Abstractions
{
    public interface IWriteOperations<T>
    {
        public void PerformOperation(T operation);
    }
}
