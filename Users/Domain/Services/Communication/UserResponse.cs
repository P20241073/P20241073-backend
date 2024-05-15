using Shared.Domain.Services.Communication;
using Users.Domain.Model;

namespace Users.Domain.Services.Communication;

public class CustomerResponse : BaseResponse<User> {
    public CustomerResponse(User resource) : base(resource) { }

    public CustomerResponse(string message) : base(message) { }
    
}