using Reports.Domain.Model;
using Shared.Domain.Services.Communication;

namespace Reports.Domain.Services.Communication;

public class ReportResponse : BaseResponse<Report>
{
    public ReportResponse(Report resource) : base(resource) { }

    public ReportResponse(string message) : base(message) { }
}
