using AutoMapper;

namespace Shared.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {

        // CreateMap<UpdateRequest, User>()
        //     .ForAllMembers(options => options.Condition(
        //         (source, target, property) =>
        //         {
        //             if (property == null) return false;
        //             if (property.GetType() == typeof(string) && string.IsNullOrEmpty((string)property)) return false;
        //             return true;
        //         }
        //     ));
    }
}