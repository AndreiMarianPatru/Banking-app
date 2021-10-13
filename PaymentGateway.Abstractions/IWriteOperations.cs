namespace PaymentGateway.Abstractions
{
    public interface IWriteOperations<T>
    {
        public interface IWriteOperation<TCommand>
        {
            void PerformOperation(TCommand operation);
        }
    }
}
