using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using WebApplication1.DataSimulation;
using WebApplication1.model;
namespace WebApplication1.Controllers
{

    /// <summary>
    /// All the function relative with the Get .
    /// </summary>
    [Authorize]
    [Route("api/StudentsController", Name = "StudentsController")]
    [ApiController]
    public partial class StudentsController : ControllerBase
    {
        [ProducesResponseType(typeof(Students), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        [HttpGet("GetStudentInfoByID", Name = "GetStudentInfoByID")]
        public ActionResult<Students> GetStudentInfoByID(int Id)
        {
            if (!ValidToken())
                return Unauthorized();

            var studentId = User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(studentId))
                return Unauthorized();

            if (!int.TryParse(studentId, out int id))
                return Unauthorized();

            if (Id <= 0)
                return BadRequest("Invalid student ID.");

            var student = AllDataStudents
                .FirstOrDefault(student => student.Id == Id);

            if (student is null)
                return NotFound();

            // Admin can see any student.
            // Normal students can only see themselves.
            if (!User.IsInRole("ADMIN") && id != Id)
                return Forbid();

            return Ok(student);
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "ADMIN")]
        [HttpGet("GetStudentByAgeFromTo", Name = "GetStudentByAgeFromTo")]
        public ActionResult<IEnumerable<Students>> GetStudentByAgeFromTo(int from, int to)
        {

            if (!ValidToken()) return Unauthorized();

            if (from < 0 || to < 0 || from > to)
                return BadRequest("Invalid age range.");

            var students = AllDataStudents
                .Where(student => student.Age >= from && student.Age <= to)
                .ToList();

            if (students.Count == 0)
                return NoContent();

            return Ok(students);
        }



        [ProducesResponseType(typeof(List<Students>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "ADMIN")]
        [HttpGet("GetAllStudents", Name = "GetAllStudents")]
        public ActionResult<List<Students>> GetAllStudents()
        {

            if (!ValidToken()) return Unauthorized();

            if (AllDataStudents is null || AllDataStudents.Count == 0)
            {
                return NoContent();
            }

            return Ok(AllDataStudents);

        }


        [ProducesResponseType(typeof(List<Students>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "ADMIN")]
        [HttpGet("GetPassStudrnts", Name = "GetPassStudrnts")]
        public ActionResult<IEnumerable<Students>> GetPassStudrnts()
        {
            if (!ValidToken()) return Unauthorized();

            var PassStudents = AllDataStudents.Where(student => student.Grad >= 50);
            if (PassStudents is null || PassStudents.ToList().Count <= 0)
                return this.NoContent();
            else
                return this.Ok(PassStudents);
        }

        [ProducesResponseType(typeof(List<Students>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "ADMIN")]
        [HttpGet("GetAllFailuresStudents", Name = "GetAllFailuresStudents")]
        public ActionResult<IEnumerable<Students>> GetAllFailuresStudents()
        {

            if (!ValidToken()) return Unauthorized();

            var FailuresStudents = AllDataStudents.Where(stuedent => stuedent.Grad < 50);
            if (FailuresStudents is null || FailuresStudents.ToList().Count <= 0)
                return this.NoContent();
            else
                return this.Ok(FailuresStudents);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<Students>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "ADMIN")]
        [HttpGet("GetStudentsHowHaveGraterThanTheGrade", Name = "GetStudentsHowHaveGraterThanTheGrade")]
        public ActionResult<IEnumerable<Students>> GetStudentsHowHaveGraterThanTheGrade(int? grade)
        {

            if (!ValidToken()) return Unauthorized();

            if (grade is null || grade <= 0) return this.BadRequest();

            var students = AllDataStudents.Where(stuedent => stuedent.Grad >= grade);

            if (students is null) return NoContent();
            else return Ok(students);

        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<Students>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "ADMIN")]
        [HttpGet("GetStudentsHowHaveLessThanTheGrade", Name = "GetStudentsHowHaveLessThanTheGrade")]
        public ActionResult<IEnumerable<Students>> GetStudentsHowHaveLessThanTheGrade(int? grade)
        {

            if (!ValidToken()) return Unauthorized();

            if (grade is null || grade <= 0) return this.BadRequest();

            var students = AllDataStudents.Where(stuedent => stuedent.Grad < grade);

            if (students is null) return NoContent();
            else return Ok(students);

        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<Students>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "ADMIN")]
        [HttpGet("GetStudentsHowHaveGradeFromTo", Name = "GetStudentsHowHaveGradeFromTo")]
        public ActionResult<IEnumerable<Students>> 
            GetStudentsHowHaveGradeFromTo(
            int? FromLessGrade, 
            int? ToGrateGrade)
        {

            if (!ValidToken()) return Unauthorized();

            if (FromLessGrade is null || FromLessGrade <= 0) return BadRequest();
            if (ToGrateGrade is null || ToGrateGrade <= 0) return BadRequest();

            var students = AllDataStudents.Where(stuedent => stuedent.Grad >= FromLessGrade && stuedent.Grad <= ToGrateGrade);

            if (students is null) return NoContent();
            else return this.Ok(students);

        }


        [ProducesResponseType(typeof(List<Students>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "ADMIN")]
        [HttpGet("GetAvgGradeForAllStudents", Name = "GetAvgGradeForAllStudents")]
        public ActionResult<double> GetAvgGradeForAllStudents()
        {
            if (!ValidToken()) return BadRequest();
            if (AllDataStudents is null) return this.NoContent();
            else return this.Ok(AllDataStudents.Select(student => student.Grad).Average());
        }


        [ProducesResponseType(typeof(Students), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        [HttpGet("GetStudentByID", Name = "GetStudentByID")]
        public ActionResult<Students> GetStudentByID(int Id)
        {
            if (!ValidToken())
                return Unauthorized();

            var studentId = User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(studentId))
                return Unauthorized();

            if (!int.TryParse(studentId, out int id))
                return Unauthorized();

            if (Id <= 0)
                return BadRequest("Invalid student ID.");

            var student = AllDataStudents
                .FirstOrDefault(student => student.Id == Id);

            if (student is null)
                return NotFound("Student not found.");

            // Admin can access any student.
            if (User.IsInRole("ADMIN"))
                return Ok(student);

            // Normal student can only access their own record.
            if (student.Id == id)
                return Ok(student);

            return Forbid();
        }


        [ProducesResponseType(typeof(Students), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [EnableRateLimiting("api")]
        [Authorize]
        [HttpGet("me", Name = "GetMyInfo")]
        public ActionResult<Students> GetMyInfo()
        {
            if (!ValidToken())
                return Unauthorized();

            var studentId = User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(studentId))
                return Unauthorized();

            if (!int.TryParse(studentId, out int id))
                return BadRequest();

            var me = AllDataStudents
                .FirstOrDefault(student => student.Id == id);

            if (me is null)
                return NotFound();

            return Ok(me);
        }

    }

    /// <summary>
    /// All the function relative with the post .
    /// </summary>
    public partial class StudentsController : ControllerBase
    {

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "ADMIN")]
        [HttpPost("AddNewStudent", Name = "AddNewStudent")]
        public ActionResult<Students> AddNewStudent(Students newStudent)
        {

            if (!ValidToken()) return Unauthorized();

            // this is the bad request option .
            if
            (
                   newStudent == null
                || newStudent.Id <= 0
                || string.IsNullOrEmpty(newStudent.Name)
                || newStudent.Grad <= 0
                || newStudent.Age <= 0
            )
            {
                return BadRequest("this is the Bad Request !");
            }

            // this is the creted option .
            else
            {
                AllDataStudents.Add(newStudent);
                return this.CreatedAtRoute("GetStudentInfoByID", new { id = newStudent.Id }, newStudent);
            }

        }

    }

    /// <summary>
    /// All the function relative with the delete .
    /// </summary>
    public partial class StudentsController : ControllerBase
    {
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("DeleteStudentById",Name= "DeleteStudentById")]
        public ActionResult? DeleteStudentById(int? id)
        {

            if(!ValidToken()) return Unauthorized();

            if (id is null || id <= 0) return BadRequest();
            if (!AllDataStudents.Any(student => student.Id == id)) return NotFound();
            else 
            {
                Students studentHowWeWantToDeletedIt = AllDataStudents.Where(student => student.Id == id).ToList()[0];
                AllDataStudents.Remove(studentHowWeWantToDeletedIt);
                if (!AllDataStudents.Any(student => student.Id == id)) return Ok();
                return StatusCode(500, "Internal Server Error");
            }
        }

    }

    /// <summary>
    /// All the function relative with the put .
    /// </summary>
    public partial class StudentsController : ControllerBase
    {

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "ADMIN")]
        [HttpPut("UpdateStudent", Name = "UpdateStudent")]
        public ActionResult UpdateStudent(Students NewStudent)
        {

            if (!ValidToken()) return Unauthorized();

            if (AllDataStudents is null) return NotFound();
            if (!NewStudent.IsValid()) return BadRequest();
            if (!AllDataStudents.Select(student => student.Id).Any(id => id == NewStudent.Id)) return NotFound();

            Students? OldStudent = AllDataStudents.FirstOrDefault(student => student.Id == NewStudent.Id);
            if (OldStudent is null) return NotFound();
            else
            {
                OldStudent.ConvertTo(NewStudent);
                return Ok();
            }

        }

    }


    /// <summary>
    /// Utility functions required for use within the StudentController.
    /// </summary>
    public partial class StudentsController
    {

        //private static List<Students> AllDataStudents =
        //    WebApplication1.DataSimulation.clsDataSimulation.Students
        //        .Select(student => new Students
        //        {
        //            Id = student.Id,
        //            Name = student.Name,
        //            Age = student.Age,
        //            Email = student.Email,
        //            Grad = student.Grad
        //        }).ToList();

        private static List<Students> AllDataStudents =
          WebApplication1.DataSimulation.clsDataSimulation.Students;

        private readonly IConfiguration _configuration;

        public StudentsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private bool ValidToken()
        {

            string? studentId = User.FindFirst("sub")?.Value;
            string? studentEmail = User.FindFirst("email")?.Value;

            if (studentId is null || studentEmail is null)
                return false;

            var student =
                DataSimulation.clsDataSimulation.Students
                .FirstOrDefault(student =>
                    student.Id.ToString() == studentId &&
                    student.Email == studentEmail);

            if (student is null) return false;
            else return student.RefreshTokenRevokedAt is null;

        }

    }

}
