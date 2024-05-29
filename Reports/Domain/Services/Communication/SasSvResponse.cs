using Reports.Domain.Model;
using Shared.Domain.Services.Communication;

namespace Reports.Domain.Services.Communication;

public class SasSvResponse : BaseResponse<SasSv>
{
    public SasSvResponse(SasSv resource) : base(resource) { }

    public SasSvResponse(string message) : base(message) { }
}