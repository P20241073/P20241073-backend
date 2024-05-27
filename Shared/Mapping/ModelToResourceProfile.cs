using AutoMapper;
using Users.Domain.Model;
using Users.Resources;
namespace Shared.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Device, DeviceResource>();
    }

}