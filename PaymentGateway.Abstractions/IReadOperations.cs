namespace PaymentGateway.Abstractions
{
    public interface IReadOperation<TInput, TResult>
    {
        TResult PerformOperation(TInput query);
    }
}
