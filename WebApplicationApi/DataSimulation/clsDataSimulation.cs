
using WebApplication1.model;
namespace WebApplication1.DataSimulation
{
    public class clsDataSimulation
    {
        public static List<Students> Students = 
            new List<model.Students>
            {
                new Students()
                {
                    Id = 1,
                    Name = "Ali Jasem",
                    Age = 23,
                    Grad = 78,
                    Email = "alil@gmail.com",
                    HashPassword = BCrypt.Net.BCrypt.HashPassword("123"),
                    Role = Roles.Role.STUDENT
                },
                new Students()
                {
                    Id = 2,
                    Name = "sousen ramee",
                    Age = 34,
                    Grad = 89,
                    Email = "sousen@gmail.com",
                    HashPassword = BCrypt.Net.BCrypt.HashPassword("123"),
                    Role = Roles.Role.STUDENT
                },
                new Students()
                {
                    Id = 3,
                    Name = "Ali Slmaan",
                    Age = 25,
                    Grad = 90,
                    Email = "alil@gmail.com",
                    HashPassword = BCrypt.Net.BCrypt.HashPassword("123"),
                    Role = Roles.Role.STUDENT
                },
                new Students()
                {
                    Id = 4,
                    Name = "Lala monner",
                    Age = 23,
                    Grad = 67, 
                    Email = "Lala@gmail.com",
                    HashPassword = BCrypt.Net.BCrypt.HashPassword("123"),
                    Role = Roles.Role.STUDENT
                },
                new Students()
                {
                    Id = 5,
                    Name = "slwaa nazen",
                    Age = 31,
                    Grad = 80,
                    Email = "Slwaa@gmail.com",
                    HashPassword = BCrypt.Net.BCrypt.HashPassword("123"),
                    Role = Roles.Role.STUDENT
                },
                new Students()
                {
                    Id = 9,
                    Name = "sereen",
                    Age = 23,
                    Grad = 78,
                    Email = "sereen@gmail.com",
                    HashPassword = BCrypt.Net.BCrypt.HashPassword("123sfv"),
                    Role = Roles.Role.ADMIN
                }
            };

        //public List<clsStudents> Students = null;

    }
}
