
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace ConsoleApp_ClintTest_0.Students
{
    /// <summary>
    /// this for the get 
    /// </summary>
    public partial class Students
    {

       

        internal static async Task<bool> GetMyInformation(string url)
        {

            if (!clsUtility.IsValidUrl(url))
                return false;

            var result = await Students.StudentClient!.GetFromJsonAsync<Students>(url);

            if (result is null) return false;

            PrintStudent(result);
            return true;

        }

        public static async Task<bool> GetAll(string url)
        {
            if (!clsUtility.IsValidUrl(url))
                return false;

            var result = await Students.StudentClient!.GetFromJsonAsync<List<Students>>(url);

            if (result is null) return false;

            PrintAllStudents(result);

            return true;
        }

        public static void PrintStudent(Students? student)
        {
            if (student is null)
                return;

            Console.WriteLine("╔══════════════════════════════╗");
            Console.WriteLine("║        Student Info          ║");
            Console.WriteLine("╠══════════════════════════════╣");
            Console.WriteLine($"║ ID   : {student.Id,-22}║");
            Console.WriteLine($"║ Name : {student.Name,-22}║");
            Console.WriteLine($"║ Age  : {student.Age,-22}║");
            Console.WriteLine($"║ Grad : {student.Grad,-22}║");
            Console.WriteLine("╚══════════════════════════════╝");
        }

        private static void PrintAllStudents(List<Students>? students)
        {
            Console.Clear();

            // ==============================
            // Header
            // ==============================
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                       ALL STUDENTS                           ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");

            Console.ResetColor();
            Console.WriteLine();
            
            // ==============================
            // Empty list
            // ==============================
            if (students == null || students.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No students found.");
                Console.ResetColor();

                Console.WriteLine();
                Console.WriteLine("Press any key to return to the main menu...");
                Console.ReadKey();

                return;
            }

            // ==============================
            // Table
            // ==============================
            const string separator =
                "──────────────────────────────────────────────────────────────";

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("{0,-7}{1,-25}{2,-10}{3,-10}",
                "ID",
                "Name",
                "Age",
                "Grade");

            Console.WriteLine(separator);

            Console.ResetColor();

            // ==============================
            // Students
            // ==============================
            foreach (Students student in students)
            {
                string name = 
                    (student.Name?.Length > 23)? 
                    student.Name[..23] + ".."
                    : student.Name ?? string.Empty;

                Console.WriteLine(
                    "{0,-7}{1,-25}{2,-10}{3,-10}",
                    student.Id,
                    name,
                    student.Age,
                    student.Grad
                );
            }

            // ==============================
            // Footer
            // ==============================
            Console.WriteLine(separator);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Total Students: {students.Count}");
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }

        public static async Task GetPassStudent(string url)
        {

            if (string.IsNullOrEmpty(url)) return;

            string JsonFile = await Students.StudentClient!.GetStringAsync(url);

            List<Students>? students = JsonSerializer.Deserialize<List<Students>>(JsonFile);

            PrintAllStudents(students);

        }

        public static async Task GetAllFailuresStudents(string url)
        {
            string jsonFile = await Students.StudentClient!.GetStringAsync(url);
            List<Students>? students = (List<Students>?)JsonSerializer.Deserialize(jsonFile, typeof(List<Students>));
            if (students != null) PrintAllStudents(students);
        }

        public static async Task GetStudentsHowHaveGraterThanTheGrade(int Grade)
        {
            string url = @"https://localhost:7079/api/StudentsController/GetStudentsHowHaveGraterThanTheGrade?grade=" + Grade;
            string jsonFile = await Students.StudentClient!.GetStringAsync(url);
            List<Students>? students = ((List<Students>?)JsonSerializer.Deserialize(jsonFile, typeof(List<Students>)));
            if (students != null) PrintAllStudents(students);
        }

        public static async Task GetStudentsHowHaveLessThanTheGrade(int Grade)
        {
            string url = @"https://localhost:7079/api/StudentsController/GetStudentsHowHaveLessThanTheGrade?grade=" + Grade;
            string jsonFile = await Students.StudentClient!.GetStringAsync(url);
            List<Students>? students = ((List<Students>?)JsonSerializer.Deserialize(jsonFile, typeof(List<Students>)));
            if (students != null) PrintAllStudents(students);
        }

        public static async Task GetStudentsHowHaveGradeFromTo(int FromLessGrade, int ToGrateGrade)
        {
            string url = $@"https://localhost:7079/api/StudentsController/GetStudentsHowHaveGradeFromTo?FromLessGrade={FromLessGrade}&ToGrateGrade={ToGrateGrade}";
            string jsonFile = await Students.StudentClient!.GetStringAsync(url);
            List<Students>? students = (List<Students>?)JsonSerializer.Deserialize(jsonFile, typeof(List<Students>));
            if (students != null) PrintAllStudents(students);
        }

        public static async Task<(double? Average, HttpStatusCode StatusCode)> GetAvgGradeForAllStudents()
        {
            const string url =
                "https://localhost:7079/api/StudentsController/GetAvgGradeForAllStudents";

            using var response = await Students.StudentClient!.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return (null, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();

            if (!double.TryParse(content, out double average))
                return (null, response.StatusCode);

            return (average, response.StatusCode);
        }


        public static async Task<Students?> GetStudentByID(string url)
        {

            Students? student = null;

            if (!string.IsNullOrEmpty(url))
            {
                var response = await Students.StudentClient!.GetAsync(url);
                if (response.StatusCode == HttpStatusCode.OK)
                    student = await Students.StudentClient!.GetFromJsonAsync<Students>(url);
            }

            return student;

        }

        public static async Task<Students?> GetStudentByID(int studentID)
        {

            Students? student = null;

            string url = $"https://localhost:7079/api/StudentsController/GetStudentInfoByID?id={studentID}";
            var response = await Students.StudentClient!.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
                student = await Students.StudentClient!.GetFromJsonAsync<Students>(url);

            return student;

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

