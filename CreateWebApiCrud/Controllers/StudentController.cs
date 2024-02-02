using CreateWebApiCrud.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CreateWebApiCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly string connectionString;
        public StudentController(IConfiguration configuration)
        {
            connectionString = configuration["ConnectionStrings:SqlServerDb"] ?? "";
        }
        [HttpPost]
        public IActionResult CreateSrudent (StudentModel stm)
        {
            try
            {
              using (var connetion = new SqlConnection(connectionString)) 
                {
                    connetion.Open();
                    string sql = "insert into tbl_StudentDetail(fname,lname,username,city,state,zip) values(@fname,@lname,@username,@city,@state,@zip)";
                    using (var command= new SqlCommand(sql, connetion))
                    {
                        command.Parameters.AddWithValue("@fname", stm.fname);
                        command.Parameters.AddWithValue("@lname", stm.lname);
                        command.Parameters.AddWithValue("@username", stm.username);
                        command.Parameters.AddWithValue("@city", stm.city);
                        command.Parameters.AddWithValue("@state", stm.state);
                        command.Parameters.AddWithValue("@zip", stm.zip);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Student", "Sorry ,but Data can't inserted !");
                return BadRequest(ModelState);
            }
            return Ok();
        }
        [HttpGet]
        public IActionResult GetStudent()
        {
            List<Student> students = new List<Student>();
            try
            {
                using (var connetion = new SqlConnection(connectionString))
                {
                    connetion.Open();
                    string sql = "select * from tbl_StudentDetail order by sid desc";
                    using (var command=new SqlCommand(sql, connetion))
                    {
                        using (var reader = command.ExecuteReader()) 
                        {
                         while (reader.Read()) {
                                Student std = new Student();

                                std.sid = reader.GetInt32(0);
                                std.fname = reader.GetString(1);
                                std.lname = reader.GetString(2);
                                std.username = reader.GetString(3);
                                std.city = reader.GetString(4);
                                std.state = reader.GetString(5);
                                std.zip = reader.GetString(6);

                                students.Add(std);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Student", "Sorry ,but Data can't Show !");
                return BadRequest(ModelState);
            }
            return Ok(students);
        }
        [HttpGet("sid")]
        public IActionResult GetStudent(int sid)
        {
            Student std = new Student();
            try
            {
                using (var connetion = new SqlConnection(connectionString))
                {
                    connetion.Open();
                    string sql = "select * from tbl_StudentDetail where sid=@sid order by sid desc";
                    using (var command = new SqlCommand(sql, connetion))
                    {
                        command.Parameters.AddWithValue("@sid", sid);
                        using (var reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                std.sid = reader.GetInt32(0);
                                std.fname = reader.GetString(1);
                                std.lname = reader.GetString(2);
                                std.username = reader.GetString(3);
                                std.city = reader.GetString(4);
                                std.state = reader.GetString(5);
                                std.zip = reader.GetString(6);
                            }
                            else
                            {
                                return NotFound();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Student", "Sorry ,but Data can't Show !");
                return BadRequest(ModelState);
            }
            return Ok(std);
        }
        [HttpPost("sid")]
        public IActionResult UpdateStudent(int sid,StudentModel stm)
        {
            try
            {
                using (var connetion = new SqlConnection(connectionString))
                {
                    connetion.Open();
                    string sql = "update tbl_StudentDetail set fname=@fname,lname=@lname,username=@username,city=@city,state=@state,zip=@zip where sid=@sid;\r\n";
                    using(var command=new SqlCommand(sql, connetion))
                    {
                        command.Parameters.AddWithValue("@fname", stm.fname);
                        command.Parameters.AddWithValue("@lname", stm.lname);
                        command.Parameters.AddWithValue("@username", stm.username);
                        command.Parameters.AddWithValue("@city", stm.city);
                        command.Parameters.AddWithValue("@state", stm.state);
                        command.Parameters.AddWithValue("@zip", stm.zip);
                        command.Parameters.AddWithValue("@sid", sid);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Student", "Sorry ,but Data can't Updated !");
                return BadRequest(ModelState);
            }
            return Ok(stm);
        }
        [HttpDelete("sid")]
        public IActionResult DeleteStudent(int sid)
        {
            try
            {
                using (var connetion = new SqlConnection(connectionString))
                {
                    connetion.Open();
                    string sql = "delete from tbl_StudentDetail where sid=@sid;\r\n";
                    using (var command = new SqlCommand(sql, connetion))
                    {
                        command.Parameters.AddWithValue("@sid",sid);
                        command.ExecuteNonQuery();
                    }
                }

             }
            catch (Exception ex)
            {
                ModelState.AddModelError("Student", "Sorry ,but Data can't Delate !");
            }
                return Ok();
        }
    }
}

