using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_API_ADO.NET_StoreProcedure_APIConsume_Ajax.DAL;
using Student_API_ADO.NET_StoreProcedure_APIConsume_Ajax.Models;
using System.Collections.Specialized;
using System.Data;

namespace Student_API_ADO.NET_StoreProcedure_APIConsume_Ajax.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentDAL studentdal;

        public StudentsController(StudentDAL studentdal)
        {
            this.studentdal = studentdal;
        }

        [HttpGet("GetAllStudent")]
        public IActionResult GetAllStudent()
        {
            try
            {
                NameValueCollection parameters = new NameValueCollection();
                DataTable data = studentdal.GetData("GetAllStudent", parameters);

                //check data retrive
                if (data != null && data.Rows.Count > 0)
                {
                    List<Student> studentlist = new List<Student>();
                    foreach (DataRow row in data.Rows)
                    {
                        Student student = new Student
                        {
                            StID = Convert.ToInt32(row["Sid"]),
                            StName = row["Sname"].ToString(),
                            StEmail = row["Semail"].ToString(),
                            StPassword = row["Spassword"].ToString(),
                            StContact = row["Scontact"].ToString()
                        };
                        studentlist.Add(student);
                    }
                    return Ok(studentlist);
                }
                else
                {
                    return NotFound("Record Not Found");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Error {ex}");
            }
        }

        [HttpGet("GetStudentById/{id}")]
        public IActionResult GetStudentById(int id)
        {
            NameValueCollection parameters = new NameValueCollection();
            DataTable data = studentdal.GetStudentByID("GetAllStudentByID", parameters, id);

            try
            {
                //check data retrive
                if (data != null && data.Rows.Count > 0)
                {
                    DataRow row = data.Rows[0];
                    Student student = new Student
                    {
                        StID = Convert.ToInt32(row["Sid"]),
                        StName = row["Sname"].ToString(),
                        StEmail = row["Semail"].ToString(),
                        StPassword = row["Spassword"].ToString(),
                        StContact = row["Scontact"].ToString()
                    };
                    return Ok(student);
                }
                else
                {
                    return NotFound("Record Not Found");
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal Error {ex}");
            }
        }

        [HttpPost("CreateStudent")]
        public IActionResult CreateStudent([FromBody] Student model)
        {
            NameValueCollection parameters = new NameValueCollection();
            DataTable data = studentdal.CreateStudent("CreateStudent", parameters, model);

            parameters.Add("@sname", model.StName);
            parameters.Add("@semail", model.StEmail);
            parameters.Add("@spassword", model.StPassword);
            parameters.Add("@scontact", model.StContact);
            
            return Ok("Data Save");

        }


        [HttpDelete("DeleteStudent/{id}")]
        public IActionResult DeleteStudent(int id)
        {
            NameValueCollection parameters = new NameValueCollection();
            DataTable data = studentdal.DeleteStudent("DeleteStudent", parameters, id);
            try
            {
                return Ok("Deleted Data");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Error {ex}"); throw;
            }
        }

        [HttpPut("EditStudent/{id}")]
        public IActionResult EditStudent([FromBody] Student model, int id)
        {
            NameValueCollection parameters = new NameValueCollection();
            

            parameters.Add("@sname", model.StName);
            parameters.Add("@semail", model.StEmail);
            parameters.Add("@spassword", model.StPassword);
            parameters.Add("@scontact", model.StContact);

            DataTable data = studentdal.EditStudent("UpdateStudent", parameters, model, id);

            return Ok("Data Updated Done");
        }

    }
}
