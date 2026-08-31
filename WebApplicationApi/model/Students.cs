namespace WebApplication1.model
{
    public class Students
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public int Grad { get; set; }

        public Students(int id,string name,int age,int Grad)
        {
            this.Id = id;
            this.Name = name;
            this.Age = age;
            this.Grad = Grad;
        }

        public bool IsValid()
        {
            if
            (
                   this.Id == 0 
                || !string.IsNullOrEmpty(this.Name)
                || this.Age > 0
                || this.Grad > 0
            ) return true;
            else return false;
        }

        public void ConvertTo(Students student)
        {
            this.Name = student.Name;
            this.Age = student.Age;
            this.Grad = student.Grad;
        }

    }
}
