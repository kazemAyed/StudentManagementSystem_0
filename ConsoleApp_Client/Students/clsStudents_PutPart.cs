using System;
using System.Text;
using System.Text.Json;

namespace ConsoleApp_ClintTest_0.Students
{

    /// <summary>
    /// this part for the PUT Part
    /// </summary>
    public partial class Students
    {

        public static async Task<bool> UpdateStudent(Students student)
        {

            if (student == null) return false;

            string url = "https://localhost:7079/api/StudentsController/UpdateStudent";
            string StudentObjAsJsonFile = JsonSerializer.Serialize(student);
            var content = new StringContent(StudentObjAsJsonFile, Encoding.UTF8, "application/json");
            var Response = await Students.StudentClient!.PutAsync(url, content);
            return Response.StatusCode == System.Net.HttpStatusCode.OK;

        }

    }

}
