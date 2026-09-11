using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ConsoleApp_ClintTest_0.Students
{
    /// <summary>
    /// this part for the post
    /// </summary>
    public partial class Students 
    {

        internal static async Task<bool> CreatedNewStudent_v00(Students? newStudent)
        {

            if (newStudent == null) return false;

            string jsonFileAsBodyContentFromTheClintToServer = JsonSerializer.Serialize(newStudent);

            if (string.IsNullOrEmpty(jsonFileAsBodyContentFromTheClintToServer)) return false;
            else
            {

                string url = @"https://localhost:7079/api/StudentsController/Post_NewStudent_v02";

                using HttpClient client = new HttpClient();

                StringContent content = new StringContent(jsonFileAsBodyContentFromTheClintToServer, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);

                ////////////////////////////////////////////////////////////////
                //// this is the part i retunted by it the student opject . ////
                ////////////////////////////////////////////////////////////////
                {
                    //Students? BackResponsingStudent = await Students.GetStudentByID_01(newStudent!.Id);

                    //if (BackResponsingStudent != null)
                    //    return BackResponsingStudent.IsSameOrEqualThis(newStudent);
                    //else
                        return false;
                }

            }
           
        }

        internal static async Task<bool> CreatedNewStudent(Students? newStudent)
        {
            
            if (newStudent == null) return false;
            else
            {

                string url = @"https://localhost:7079/api/StudentsController/AddNewStudent";
                string jsonFileAsBodyContant = JsonSerializer.Serialize(newStudent);

                HttpContent content = new StringContent(jsonFileAsBodyContant, Encoding.UTF8, "application/json");
                HttpResponseMessage ResponseMessage = await Students.StudentClient!.PostAsync(url, content);

                return ResponseMessage.StatusCode == System.Net.HttpStatusCode.Created;

            }

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

