using Grpc.Core;

namespace GrpcServer.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;
        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();
          
            switch (request.UserId)
            {
                case 1:
                    output.FirtName = "Maykl";
                    output.LastName = "Jekson";
                    break;
                case 2:
                    output.FirtName = "Pavel";
                    output.LastName = "Durov";
                    break;
                case 3:
                    output.FirtName = "Mark";
                    output.LastName = "Suberbek";
                    break;
                default:
                    output.FirtName = "Elbek";
                    output.LastName = "Abdurahimov";
                    break;
            }
            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(
            NewCustomerRequest request,
            IServerStreamWriter<CustomerModel> responseStream,
            ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>()
            {
                new CustomerModel()
                {
                    FirtName = "Maykl",
                    LastName = "Jekson",
                    EmailAddress ="maykljeakson@gmail.com",
                    IsActive = true
                },
                new CustomerModel()
                {
                    FirtName = "Pavel",
                    LastName = "Durov",
                    EmailAddress ="paveldurov@gmail.com",
                    IsActive = false
                },
                new CustomerModel()
                {
                    FirtName = "Mark",
                    LastName = "Suberbek",
                    EmailAddress ="marksuberbek@gmail.com",
                    IsActive = false
                },
                new CustomerModel()
                {
                    FirtName = "Elbek",
                    LastName = "Abdurahimov",
                     EmailAddress ="elbekabdurahimov@gmail.com",
                    IsActive = true
                }
            };

            foreach (var customer in customers)
            {
                await  Task.Delay(1000);
                await responseStream.WriteAsync(customer);
            }
        }
    }
}
