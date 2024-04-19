using Grpc.Core;

namespace StudentServer.Services;

public class StudentService : Student.StudentBase
{
    public override async Task GetNewStudents(
        NewStudentRequest request,
        IServerStreamWriter<StudentModel> responseStream,
        ServerCallContext context)
    {
        var students = new List<StudentModel>()
        {
            new StudentModel()
            {
                FirtName = "Maykl",
                LastName = "Jekson",
                FatherName = "Jeck Ali",
                Address = "American Washington city",
                EmailAddress ="maykljeakson@gmail.com",
                Phone = "+344445623",
                IsActive = true,
                Course = 1
            },
            new StudentModel()
            {
                FirtName = "Pavel",
                LastName = "Durov",
                FatherName = "Jeck Ali",
                Address = "Russia Moscow city",
                EmailAddress ="paveldurov@gmail.com",
                Phone = "+791555555420",
                IsActive = false,
                Course = 2
            },
            new StudentModel()
            {
                FirtName = "Mark",
                LastName = "Suberbek",
                FatherName = "",
                Address =  "",
                EmailAddress ="marksuberbek@gmail.com",
                Phone = "+564111111",
                IsActive = false,
                Course = 2
            },
            new StudentModel()
            {
                FirtName = "Elbek",
                LastName = "Abdurahimov",
                FatherName = "Pirnazar o'g'li",
                Address = "Qashqadaryo viloyati Qamashi tumani",
                EmailAddress ="elbekabdurahimov@gmail.com",
                Phone = "+998715555555",
                IsActive = true,
                Course = 4
            }
        };

        foreach (var student in students)
        {
            await Task.Delay(1000);
            await responseStream.WriteAsync(student);
        }
    }
}
