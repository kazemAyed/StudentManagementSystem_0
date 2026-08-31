using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleApp_ClintTest_0.Students
{
    public partial class Students
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("age")]
        public int Age { get; set; }

        [JsonPropertyName("grad")]
        public int Grad { get; set; }

        public Students()
        { }

        public Students(int id, string name, int age, int Grad)
        {
            this.Id = id;
            this.Name = name;
            this.Age = age;
            this.Grad = Grad;
        }

    }
}
