namespace ConsoleApp_ClintTest_0.Students
{
    /// <summary>
    /// this part for the deleting .
    /// </summary>
    public partial class Students
    {
       
        public class DeleteById_00 
        {
            
            public static string? ResponseFromDeletionBody { get; set; }
            public static ICollection<string>? DetailsAboutTheHeader { get; private set; }
            public static async Task<bool> DeleteStudentById(int id)
            {
                if (id <= 0) return false;

                string jsonFile = $@"https://localhost:7079/api/StudentsController/DeleteStudentById?id={id}";
                HttpResponseMessage Response = await Students.StudentClient!.DeleteAsync(jsonFile);
                if (Response != null)
                {
                    DetailsAboutTheHeader = Response.Content.Headers.Allow;
                    return Response.StatusCode == System.Net.HttpStatusCode.OK;
                }
                return false;
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

