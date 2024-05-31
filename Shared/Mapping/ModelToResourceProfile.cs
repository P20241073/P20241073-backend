using Activities.Domain.Model;
using Activities.Resources;
using AutoMapper;
using Reports.Domain.Model;
using Reports.Resources;
using Users.Domain.Model;
using Users.Resources;
namespace Shared.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Device, DeviceResource>();
        CreateMap<Activity, ActivityResource>();
        CreateMap<SasSv, SasSvResource>();
    }

}