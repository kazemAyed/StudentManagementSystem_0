using System.Diagnostics.Metrics;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ConsoleApp_ClintTest_0.Students
{
    public partial class Students
    {

        public bool IsSameOrEqualThis(Students? other)
        {
            if (other != null && this != null)
            {
                if
                (
                    this.Id == other.Id
                    && this.Name == other.Name
                    && this.Age == other.Age
                    && this.Grad == other.Grad

                ) return true;
                else return false;
            }
            else return false;
        }

        public static async Task<int> GetMaxIDInStudentsCollection()
        {
            int maxID = 0;
            using (HttpClient client = new HttpClient())
            {
                string url = @"https://localhost:7079/api/StudentsController/GetAllStudents";
                var listOfStudents = await client.GetFromJsonAsync<List<Students>>(url);
                if (listOfStudents != null)maxID = listOfStudents.Select(student => student.Id).Max();
            }
            return maxID;
        }

        public static async Task<int> GenerateNewStudentID()
        {
            return await Students.GetMaxIDInStudentsCollection() + 1;
        }

    }

}


/*
 
Request body::
{
  "id": 12,
  "name": "Ali mazen",
  "age": 32,
  "grad": 78
}
 
__________________________________________________

Curl ::
  curl -X 'POST' \
  'https://localhost:7079/api/StudentsController/Put_NewStudent_v02' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
        "id": 12,
        "name": "Ali mazen",
        "age": 32,
        "grad": 78
      }'
__________________________________________________

                    -----> Code [Status Code] = 201
Server response :::|
                    -----> Details

 Details::
==========
	
    Response body :: 
        {
          "id": 12,
          "name": "Ali mazen",
          "age": 32,
          "grad": 78
        }
    
    Response headers::
        content-type: application/json; charset=utf-8 
        date: Mon,29 Jun 2026 08:22:06 GMT 
        location: https://localhost:7079/api/StudentsController/GetStudentByID_01?id=12 
        server: Kestrel 


__________________________________________________

 */

