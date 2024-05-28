using Reports.Domain.Model;
using Shared.Domain.Services.Communication;

namespace Reports.Domain.Services.Communication;

public class SurveyResponse : BaseResponse<Survey>
{
    public SurveyResponse(Survey resource) : base(resource) { }

    public SurveyResponse(string message) : base(message) { }
}