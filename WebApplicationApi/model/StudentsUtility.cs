namespace WebApplication1.model
{

    public partial class Students
    {

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
