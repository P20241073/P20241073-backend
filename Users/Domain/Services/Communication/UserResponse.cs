using Users.Domain.Model;

namespace Users.Domain.Services.Communication;

public class CustomerResponse : BaseResponse<Customer> {
    public CustomerResponse(Customer resource) : base(resource) { }

    public CustomerResponse(string message) : base(message) { }
    
}