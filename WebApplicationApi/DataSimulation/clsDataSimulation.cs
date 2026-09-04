
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
                    Grad = 78
                },
                new Students()
                {
                    Id = 2,
                    Name = "sousen ramee",
                    Age = 34,
                    Grad = 89
                },
                new Students()
                {
                    Id = 3,
                    Name = "Ali Slmaan",
                    Age = 25,
                    Grad = 90
                },
                new Students()
                {
                    Id = 4,
                    Name = "Lala monner",
                    Age = 23,
                    Grad = 67
                },
                new Students()
                {
                    Id = 5,
                    Name = "slwaa nazen",
                    Age = 31,
                    Grad = 80
                },
            };

        //public List<clsStudents> Students = null;

    }
}
