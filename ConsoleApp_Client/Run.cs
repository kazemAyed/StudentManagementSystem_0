using ConsoleApp_ClintTest_0.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_ClintTest_0
{
    public class Run
    {

        public static async Task Start()
        {

            // this function to whow the exiption exactly , where is it in the exactly line in the project,
            // without the try/catch tool .
            await SubStart_0();

            // this is with the try/catch tool.
            //await SubStart_1();

        }

        static async Task SubStart_0()
        {
            await doWhileStartFunc();
        }

        static async Task SubStart_1()
        {
            try
            {
                await doWhileStartFunc();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        static async Task doWhileStartFunc()
        {
                
            StartPointPrograme:
            {
                do
                {

                    Console.Clear();

                    //this is wright the user choices .
                    Run.PrintUserChoices();

                    // this is the user choice .
                    string choice = Console.ReadLine()!;

                    switch (choice)
                    {
                        case "0":
                            {
                                goto EndProcese;
                            }
                        case "1":
                            {
                                string endPointFunctionAPI = ReadApiEndpoint();
                                Console.Clear();
                                bool combletedSuccefuly = await Students.Students.GetAll(endPointFunctionAPI);

                                if (!combletedSuccefuly) goto StartPointPrograme;

                            }
                            break;
                        case "2":
                            {
                                string endPointFunctionAPI = ReadApiEndpoint();
                                Console.Clear();
                                await Students.Students.GetPassStudent(endPointFunctionAPI);
                            }
                            break;
                        case "3":
                            {
                                string endPointFunctionAPI = ReadApiEndpoint();
                                Console.Clear();
                                await Students.Students.GetAllFailuresStudents(endPointFunctionAPI);
                            }
                            break;
                        case "4":
                            {
                                Console.Clear();
                                Console.Write("{Get Students How Have GraterThan The Grade} enter the Grade [0, 100] : ");
                                int Grade = Convert.ToInt32(Console.ReadLine());
                                Console.Clear();
                                await Students.Students.GetStudentsHowHaveGraterThanTheGrade(Grade);
                            }
                            break;
                        case "5":
                            {
                                Console.Clear();
                                Console.Write("{Get Students How Have LessThan The Grade} enter the Grade [0, 100] : ");
                                int Grade = Convert.ToInt32(Console.ReadLine());
                                Console.Clear();
                                await Students.Students.GetStudentsHowHaveLessThanTheGrade(Grade);
                            }
                            break;
                        case "6":
                            {
                                int StartGrade = 0, EndGrade = 0;
                                Console.Clear();
                                Console.WriteLine("{Get Students How Have Grade From To}");
                                Console.Write("enter start grade should by >= 0 : ");
                                StartGrade = Convert.ToInt32(Console.ReadLine());
                                Console.Write("enter end grade should by <= 100 : ");
                                EndGrade = Convert.ToInt32(Console.ReadLine());
                                Console.Clear();
                                await Students.Students.GetStudentsHowHaveGradeFromTo(StartGrade, EndGrade);
                            }
                            break;
                        case "7":
                            {
                                Console.Clear();
                                double avj = (double)await Students.Students.GetAvgGradeForAllStudents();
                                if (avj == 404) Console.WriteLine($"is not dada exist in the seerver ! Status Code is {avj}");
                                else Console.WriteLine("AVG Grade For All Students is : " + (avj).ToString());
                            }
                            break;
                        case "8":
                            {
                                Console.Clear();
                                await Students.Students.GetAll(@"https://localhost:7079/api/StudentsController/GetAllStudents");
                            }
                            break;
                        case "9":
                            {

                                string endPointFunctionAPI = ReadApiEndpoint();
                                Console.Clear();

                                if (clsUtility.IsValidUrl(endPointFunctionAPI))
                                {
                                    Students.Students? Student = await Students.Students.GetStudentByID(endPointFunctionAPI);
                                    if (Student != null)
                                        Students.Students.PrintStudent(Student);
                                    else Console.WriteLine("no content!");
                                }
                                else Console.WriteLine("the url is not valid !!");

                            }
                            break;
                        case "10":
                            {
                                await Run.CreateNewStudent();
                            }
                            break;
                        case "11":
                            {
                                await Run.DeleteStudent();
                            }
                            break;
                        case "12":
                            {
                                await Run.UpdateStudent();
                            }
                            break;
                        default:
                            {
                                // Exit
                            }
                            break;
                    }

                } while (clsUtility.Again());
            }
            
            EndProcese:
            {
                Console.WriteLine("EndProcese");
                return;
            }

        }

        private static async Task DeleteStudent()
        {
            Console.Clear();

            // ==============================
            // Header
            // ==============================
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine("║              DELETE STUDENT                  ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");
            Console.ResetColor();

            Console.WriteLine();

            // ==============================
            // Read Student ID
            // ==============================
            int studentId;

            while (true)
            {
                Console.Write("Student ID: ");

                string input = Console.ReadLine()?.Trim() ?? string.Empty;

                if (int.TryParse(input, out studentId) && studentId > 0)
                    break;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid student ID.");
                Console.ResetColor();

                Console.WriteLine();
            }

            Console.Clear();

            // ==============================
            // Confirmation
            // ==============================
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine("║              DELETE CONFIRMATION             ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine($"Student ID: {studentId}");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nWARNING: This action cannot be undone.");
            Console.ResetColor();

            Console.Write("\nAre you sure you want to delete this student? [Y/N]: ");

            string confirmation =
                Console.ReadLine()?.Trim().ToUpperInvariant() ?? "N";

            if (confirmation != "Y")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nDeletion cancelled.");
                Console.ResetColor();

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();

                return;
            }

            // ==============================
            // Delete
            // ==============================
            bool deleted =
                await Students.Students.DeleteById_00.DeleteStudentById(studentId);

            Console.WriteLine();

            if (deleted)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("╔══════════════════════════════════════════════╗");
                Console.WriteLine("║          STUDENT DELETED SUCCESSFULLY        ║");
                Console.WriteLine("╚══════════════════════════════════════════════╝");
                Console.ResetColor();

                Console.WriteLine();
                Console.WriteLine($"Student ID {studentId} was deleted.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔══════════════════════════════════════════════╗");
                Console.WriteLine("║             DELETE OPERATION FAILED          ║");
                Console.WriteLine("╚══════════════════════════════════════════════╝");
                Console.ResetColor();

                Console.WriteLine();
                Console.WriteLine($"Could not delete student with ID {studentId}.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private static async Task CreateNewStudent()
        {
            Console.Clear();

            // ==============================
            // Header
            // ==============================
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine("║              CREATE NEW STUDENT              ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");
            Console.ResetColor();

            Console.WriteLine();

            // ==============================
            // Create student
            // ==============================
            Students.Students newStudent = new()
            {
                Id = await Students.Students.GenerateNewStudentID()
            };

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"Student ID: {newStudent.Id}");
            Console.ResetColor();

            Console.WriteLine();

            // ==============================
            // Name
            // ==============================
            Console.Write("Name: ");
            string name = Console.ReadLine()?.Trim() ?? string.Empty;

            while (string.IsNullOrWhiteSpace(name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Name cannot be empty.");
                Console.ResetColor();

                Console.Write("Name: ");
                name = Console.ReadLine()?.Trim() ?? string.Empty;
            }

            newStudent.Name = name;

            // ==============================
            // Age
            // ==============================
            newStudent.Age = ReadPositiveInteger("Age");

            // ==============================
            // Grade
            // ==============================
            newStudent.Grad = ReadPositiveInteger("Grade");

            // ==============================
            // Confirmation
            // ==============================
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine("║              STUDENT INFORMATION             ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine($"  ID    : {newStudent.Id}");
            Console.WriteLine($"  Name  : {newStudent.Name}");
            Console.WriteLine($"  Age   : {newStudent.Age}");
            Console.WriteLine($"  Grade : {newStudent.Grad}");

            Console.WriteLine();
            Console.Write("Create this student? [Y/N]: ");

            string confirmation = Console.ReadLine()?.Trim().ToUpperInvariant() ?? "N";

            if (confirmation != "Y")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nStudent creation cancelled.");
                Console.ResetColor();

                return;
            }

            // ==============================
            // Save
            // ==============================
            bool created = await Students.Students.CreatedNewStudent(newStudent);

            Console.WriteLine();

            if (created)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("╔══════════════════════════════════════════════╗");
                Console.WriteLine("║          STUDENT CREATED SUCCESSFULLY       ║");
                Console.WriteLine("╚══════════════════════════════════════════════╝");
                Console.ResetColor();

                Console.WriteLine();
                Console.WriteLine($"Student ID: {newStudent.Id}");

                Console.Write("\nShow student information? [Y/N]: ");

                string showStudent =
                    Console.ReadLine()?.Trim().ToUpperInvariant() ?? "N";

                if (showStudent == "Y")
                {
                    Console.Clear();

                    // Uncomment when your PrintStudent method is ready.
                    // Students.Students? student =
                    //     await Students.Students.GetStudentByID_01(newStudent.Id);

                    // Students.Students.PrintStudent(student);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to create the student.");
                Console.ResetColor();
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private static int ReadPositiveInteger(string fieldName)
        {
            while (true)
            {
                Console.Write($"{fieldName}: ");

                string input = Console.ReadLine()?.Trim() ?? string.Empty;

                if (int.TryParse(input, out int value) && value > 0)
                {
                    return value;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Please enter a valid positive {fieldName.ToLower()}.");
                Console.ResetColor();
            }
        }

        private static async Task UpdateStudent()
        {
            Console.Clear();

            // ==============================
            // Header
            // ==============================
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine("║              UPDATE STUDENT                  ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");
            Console.ResetColor();

            Console.WriteLine();

            // ==============================
            // Get Student ID
            // ==============================
            int studentId = ReadPositiveInteger("Student ID");

            Console.WriteLine();

            // ==============================
            // Get existing student
            // ==============================
            Students.Students? student =
                await Students.Students.GetStudentByID(studentId);

            if (student == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"✗ Student with ID {studentId} was not found.");
                Console.ResetColor();

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();

                return;
            }

            // ==============================
            // Display current information
            // ==============================
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Current Student Information");
            Console.ResetColor();

            Console.WriteLine("------------------------------");
            Console.WriteLine($"ID    : {student.Id}");
            Console.WriteLine($"Name  : {student.Name}");
            Console.WriteLine($"Age   : {student.Age}");
            Console.WriteLine($"Grade : {student.Grad}");
            Console.WriteLine("------------------------------");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Enter the new information:");
            Console.WriteLine("(Press ENTER to keep the current value)");
            Console.ResetColor();

            // ==============================
            // Update Name
            // ==============================
            Console.Write($"\nName [{student.Name}]: ");
            string? nameInput = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(nameInput))
                student.Name = nameInput.Trim();

            // ==============================
            // Update Age
            // ==============================
            while (true)
            {
                Console.Write($"Age [{student.Age}]: ");

                string? ageInput = Console.ReadLine();

                // Keep old value
                if (string.IsNullOrWhiteSpace(ageInput))
                    break;

                if (int.TryParse(ageInput, out int age) && age > 0)
                {
                    student.Age = age;
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid positive age.");
                Console.ResetColor();
            }

            // ==============================
            // Update Grade
            // ==============================
            while (true)
            {
                Console.Write($"Grade [{student.Grad}]: ");

                string? gradeInput = Console.ReadLine();

                // Keep old value
                if (string.IsNullOrWhiteSpace(gradeInput))
                    break;

                if (int.TryParse(gradeInput, out int grade) && grade > 0)
                {
                    student.Grad = grade;
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid positive grade.");
                Console.ResetColor();
            }

            // ==============================
            // Show changes
            // ==============================
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine("║             CONFIRM UPDATE                   ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");
            Console.ResetColor();

            Console.WriteLine();

            Console.WriteLine($"  ID    : {student.Id}");
            Console.WriteLine($"  Name  : {student.Name}");
            Console.WriteLine($"  Age   : {student.Age}");
            Console.WriteLine($"  Grade : {student.Grad}");

            Console.WriteLine();

            Console.Write("Save these changes? [Y/N]: ");

            string confirmation =
                Console.ReadLine()?.Trim().ToUpperInvariant() ?? "N";

            if (confirmation != "Y")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nUpdate cancelled.");
                Console.ResetColor();

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();

                return;
            }

            // ==============================
            // Update student
            // ==============================
            bool updated = await Students.Students.UpdateStudent(student);

            Console.WriteLine();

            if (updated)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("╔══════════════════════════════════════════════╗");
                Console.WriteLine("║          STUDENT UPDATED SUCCESSFULLY        ║");
                Console.WriteLine("╚══════════════════════════════════════════════╝");
                Console.ResetColor();

                Console.WriteLine();
                Console.WriteLine($"Student ID {student.Id} has been updated.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔══════════════════════════════════════════════╗");
                Console.WriteLine("║             UPDATE OPERATION FAILED          ║");
                Console.WriteLine("╚══════════════════════════════════════════════╝");
                Console.ResetColor();
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private static void PrintUserChoices()
        {
            Console.WriteLine();
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║              STUDENT MANAGEMENT SYSTEM             ║");
            Console.WriteLine("╠════════════════════════════════════════════════════╣");

            Console.WriteLine("║  0. Exit                                           ║");

            Console.WriteLine("║                                                    ║");
            Console.WriteLine("║  ─────────────── Student Queries ───────────────   ║");
            Console.WriteLine("║  1. Get All Students                               ║");
            Console.WriteLine("║  2. Get Passed Students                            ║");
            Console.WriteLine("║  3. Get Failed Students                            ║");
            Console.WriteLine("║  4. Get Students With Grade Greater Than           ║");
            Console.WriteLine("║  5. Get Students With Grade Less Than              ║");
            Console.WriteLine("║  6. Get Students Within Grade Range                ║");
            Console.WriteLine("║  7. Get Average Grade                              ║");
            Console.WriteLine("║  8. Get All Students Without URL                   ║");
            Console.WriteLine("║  9. Get Student By ID                              ║");

            Console.WriteLine("║                                                    ║");
            Console.WriteLine("║  ─────────── Student Management ────────────────   ║");
            Console.WriteLine("║ 10. Create New Student                             ║");
            Console.WriteLine("║ 11. Delete Student By ID                           ║");
            Console.WriteLine("║ 12. Update Student By ID                           ║");

            Console.WriteLine("╚════════════════════════════════════════════════════╝");
            Console.Write("\nSelect an option _: ");
        }

        private static string ReadApiEndpoint()
        {
            Console.WriteLine();
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║          Enter API Endpoint          ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.Write("URL _:");

            return Console.ReadLine()?.Trim() ?? string.Empty;
        }

    }
}
