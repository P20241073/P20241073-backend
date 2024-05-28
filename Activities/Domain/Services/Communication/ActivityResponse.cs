using Activites.Domain.Model;
using Shared.Domain.Services.Communication;

namespace Activities.Domain.Services.Communication;

public class ActivityResponse : BaseResponse<Activity>
{
    public ActivityResponse(Activity resource) : base(resource) { }

    public ActivityResponse(string message) : base(message) { }
}