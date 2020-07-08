using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebAPICRUDOps.Data;
using WebAPICRUDOps.Models;
using Microsoft.Extensions.Configuration;

namespace WebAPICRUDOps.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        IConfiguration _iconfiguration;

        public EmployeesController(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }

        public IActionResult Index()
        {
            // First way  
            //string value1 = _iconfiguration.GetSection("Data").GetSection("DevConnection").Value;
            // Second way  
            string connection = _iconfiguration.GetValue<string>("Data:DevConnection");
            return null;
        }


        [HttpGet]
        [ActionName("GetEmployeeByID")]
        public Employee Get(int id)
        {
            //return listEmp.First(e => e.ID == id);  
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select * from tblEmployee where EmployeeId=" + id + "";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            Employee emp = null;
            while (reader.Read())
            {
                emp = new Employee();
                emp.EmployeeId = Convert.ToInt32(reader.GetValue(0));
                emp.Name = reader.GetValue(1).ToString();
            }
            return emp;
            myConnection.Close();
        }
    }
}