namespace FwksLabs.Libs.Core.Abstractions.Contexts;

public interface IRequestContext
{
    public string CorrelationId { get; }
    public string RequestId { get; }

    void Initialize(string correlationId, string requestId);
}