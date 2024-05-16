using AutoMapper;
using Users.Domain.Model;
using Users.Resources;

namespace Shared.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveDeviceResource, Device>();
    }
}