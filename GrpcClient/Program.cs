using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using StudentServer;

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

        Console.WriteLine("New Customer List\n");

        using(var call = customerClient.GetNewCustomers(new NewCustomerRequest()))
        {
            while (await call.ResponseStream.MoveNext())
            {
                var currentCustomer = call.ResponseStream.Current;

                Console.WriteLine($"Firt name : {currentCustomer.FirtName} | Last name : {currentCustomer.LastName} | " +
                    $"Email address : {currentCustomer.EmailAddress} | Is active : {currentCustomer.IsActive}");
            }
        }

        await Console.Out.WriteLineAsync("\nStudents informations\n");

        channel = GrpcChannel.ForAddress("https://localhost:7282");
        var studentClient = new Student.StudentClient(channel);

        using (var call = studentClient.GetNewStudents(new NewStudentRequest()))
        {
            while (await call.ResponseStream.MoveNext())
            {
                var currentStudent = call.ResponseStream.Current;

                Console.WriteLine($"Firt name : {currentStudent.FirtName} | Last name : {currentStudent.LastName} | Father name : {currentStudent.FatherName} | Address : {currentStudent.Address}" +
                    $"Email address : {currentStudent.EmailAddress} | Phone : {currentStudent.Phone} | Is active : {currentStudent.IsActive} | Course : {currentStudent.Course}");
            }
        }

        Console.ReadLine();
        #endregion
    }
}