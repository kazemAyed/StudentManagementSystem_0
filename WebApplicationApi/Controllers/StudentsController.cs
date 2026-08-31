using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Linq;
using System.Net;
using System.Text.Json;
using WebApplication1.DataSimulation;
using WebApplication1.model;
namespace WebApplication1.Controllers
{

    /// <summary>
    /// All the function relative with the Get .
    /// </summary>
    [Route("api/StudentsController", Name = "StudentsController")]
    [ApiController]
    public partial class StudentsController : ControllerBase
    {

        private static List<Students> AllDataStudents = WebApplication1.DataSimulation.clsDataSimulation.Students;

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetStudentInfoByID", Name = "GetStudentInfoByID")]
        public ActionResult<Students> GetStudentInfoByID(int? id)
        {

            if (id is null || id <= 0) return BadRequest();

            var student = AllDataStudents.FirstOrDefault(student => student.Id == id);
            
            if(student is null) return NoContent();
            else return Ok(student);

        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetStudentByAgeFromTo", Name = "GetStudentByAgeFromTo")]
        public ActionResult<IEnumerable<Students>> GetStudentByAgeFromTo(int from, int to)
        {
            var students = AllDataStudents.Where(student => student.Age >= from && student.Age <= to);
            if (students is null) return NoContent();
            else return Ok(students);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetAllStudents", Name = "GetAllStudents")]
        public object? GetAllStudents()
        {
            if (StudentsController.AllDataStudents is null || StudentsController.AllDataStudents.Count <= 0)
                return this.NoContent();
            else
                return this.Ok(AllDataStudents); 
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetPassStudrnts", Name = "GetPassStudrnts")]
        public ActionResult<IEnumerable<Students>> GetPassStudrnts()
        {
            var PassStudents = AllDataStudents.Where(student => student.Grad >= 50);
            if (PassStudents is null || PassStudents.ToList().Count <= 0)
                return this.NoContent();
            else
                return this.Ok(PassStudents);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetAllFailuresStudents", Name = "GetAllFailuresStudents")]
        public ActionResult<IEnumerable<Students>> GetAllFailuresStudents()
        {
            var FailuresStudents = AllDataStudents.Where(stuedent => stuedent.Grad < 50);
            if (FailuresStudents is null || FailuresStudents.ToList().Count <= 0)
                return this.NoContent();
            else
                return this.Ok(FailuresStudents);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetStudentsHowHaveGraterThanTheGrade", Name = "GetStudentsHowHaveGraterThanTheGrade")]
        public ActionResult<IEnumerable<Students>> GetStudentsHowHaveGraterThanTheGrade(int? grade)
        {

            if (grade is null || grade <= 0) return this.BadRequest();

            var students = AllDataStudents.Where(stuedent => stuedent.Grad >= grade);

            if (students is null) return NoContent();
            else return Ok(students);

        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetStudentsHowHaveLessThanTheGrade", Name = "GetStudentsHowHaveLessThanTheGrade")]
        public ActionResult<IEnumerable<Students>> GetStudentsHowHaveLessThanTheGrade(int? grade)
        {

            if (grade is null || grade <= 0) return this.BadRequest();

            var students = AllDataStudents.Where(stuedent => stuedent.Grad < grade);

            if (students is null) return NoContent();
            else return Ok(students);

        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetStudentsHowHaveGradeFromTo", Name = "GetStudentsHowHaveGradeFromTo")]
        public ActionResult<IEnumerable<Students>> GetStudentsHowHaveGradeFromTo(int? FromLessGrade, int? ToGrateGrade)
        {

            if (FromLessGrade is null || FromLessGrade <= 0) return BadRequest();
            if (ToGrateGrade is null || ToGrateGrade <= 0) return BadRequest();

            var students = AllDataStudents.Where(stuedent => stuedent.Grad >= FromLessGrade && stuedent.Grad <= ToGrateGrade);

            if (students is null) return NoContent();
            else return this.Ok(students);

        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetAvgGradeForAllStudents", Name = "GetAvgGradeForAllStudents")]
        public ActionResult<double> GetAvgGradeForAllStudents()
        {
            if (AllDataStudents is null) return this.NoContent();
            else return this.Ok(AllDataStudents.Select(student => student.Grad).Average());
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetStudentByID", Name = "GetStudentByID")]
        public ActionResult<Students> GetStudentByID(int? Id)
        {

            if (Id is null ||Id <= 0) return this.BadRequest("Not found the negtiv id");

            var student = AllDataStudents.FirstOrDefault(student => student.Id == Id);

            if (student is null) return this.NotFound("this is not Found!");

            else return this.Ok(student);

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("AddNewStudent", Name = "AddNewStudent")]
        public ActionResult<Students> AddNewStudent(Students newStudent)
        {

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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("DeleteStudentById",Name= "DeleteStudentById")]
        public ActionResult? DeleteStudentById(int? id)
        {
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("UpdateStudent", Name = "UpdateStudent")]
        public ActionResult UpdateStudent(Students NewStudent)
        {

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

}
