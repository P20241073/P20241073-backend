using Activities.Domain.Model;
using AutoMapper;
using Reports.Domain.Model;
using Reports.Resources;
using Users.Domain.Model;
using Users.Resources;

namespace Shared.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveDeviceResource, Device>();
        CreateMap<SaveActivityResource, Activity>();
        CreateMap<SaveSasSvResource, SasSv>();
        CreateMap<SaveReportResource, Report>();
    }
}