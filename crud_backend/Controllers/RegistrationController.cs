using crud_backend.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace crud_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public RegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("registration")]

        public string registration(Registration registration)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("StudentsConnectionString").ToString()))
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Resgistration(UserName,Password,Email,IsActive) VALUES(@UserName,@Password,@Email,@IsActive)", con);

                cmd.Parameters.AddWithValue("@UserName", registration.UserName);
                cmd.Parameters.AddWithValue("@Password", registration.Password);
                cmd.Parameters.AddWithValue("@Email", registration.Email);
                cmd.Parameters.AddWithValue("@IsActive", registration.IsActive);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();

                if (i > 0)
                {
                    return "Data Inserted";
                }
                else
                {
                    return "Error";
                }
            }
        }
    }
}