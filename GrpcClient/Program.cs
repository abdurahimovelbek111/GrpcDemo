using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;

public class Program
{
    private static async Task Main(string[] args)
    {
        #region Greet Proto 
        /* var input = new HelloRequest { Name = "Maykl Jekson" };
         var channel = GrpcChannel.ForAddress("https://localhost:7112");
         var client = new Greeter.GreeterClient(channel);
         var reply = await client.SayHelloAsync(input);

         Console.WriteLine(reply.Message);*/
        #endregion

        #region Customer Proto 
        /* mbox:
         var channel = GrpcChannel.ForAddress("https://localhost:7112");
         var customerClient = new Customer.CustomerClient(channel);
         Console.Write("User Id ni kiriting : ");
         var model = new CustomerLookupModel() { UserId = Int32.Parse(Console.ReadLine()) };

         var reply = await customerClient.GetCustomerInfoAsync(model);

         Console.WriteLine($"\nUser Id: {model.UserId}\n" +
             $"Firt name: {reply.FirtName}\n" +
             $"Last name: {reply.LastName}\n");
         Console.WriteLine("Davom etish uchun Enter tugmasini bosing!!!!");
         Console.ReadLine();
         goto mbox;*/

        var channel = GrpcChannel.ForAddress("https://localhost:7112");
        var customerClient = new Customer.CustomerClient(channel);

        Console.WriteLine("New Customer List");
        Console.WriteLine();

        using(var call = customerClient.GetNewCustomers(new NewCustomerRequest()))
        {
            while (await call.ResponseStream.MoveNext())
            {
                var currentCustomer = call.ResponseStream.Current;

                Console.WriteLine($"Firt name : {currentCustomer.FirtName} | Last name : {currentCustomer.LastName} | " +
                    $"Email address : {currentCustomer.EmailAddress} | Is active : {currentCustomer.IsActive}");
            }
        }
        Console.ReadLine();
        #endregion
    }
}