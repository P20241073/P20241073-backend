using Activities.Domain.Model;
using Activities.Resources;
using AutoMapper;
using Users.Domain.Model;
using Users.Resources;
namespace Shared.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Device, DeviceResource>();
        CreateMap<Activity, ActivityResource>();
    }

}