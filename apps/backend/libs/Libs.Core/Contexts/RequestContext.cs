using FwksLabs.Libs.Core.Abstractions.Contexts;
using FwksLabs.Libs.Core.Extensions;

namespace FwksLabs.Libs.Core.Contexts;

public sealed class RequestContext : IRequestContext
{
    private string _correlationId = string.Empty;
    private string _requestId = string.Empty;

    public string CorrelationId
    {
        get => _correlationId;

        private set
        {
            if (_correlationId.HasValue())
                return;

            _correlationId = value;
        }
    }

    public string RequestId
    {
        get => _requestId;

        private set
        {
            if (_requestId.HasValue())
                return;

            _requestId = value;
        }
    }

    public void Initialize(string correlationId, string requestId)
    {
        if (correlationId.HasValue())
            CorrelationId = correlationId;

        if (requestId.HasValue())
            RequestId = requestId;
    }
}