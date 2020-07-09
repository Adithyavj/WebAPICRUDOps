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
using System.Data.Common;

namespace WebAPICRUDOps.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase, Data.BaseDataAccess
    {
        string connection = "";
        IConfiguration _iconfiguration;
        Data.BaseDataAccess baseData;
        public EmployeesController(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
            connection = _iconfiguration.GetValue<string>("Data:DevConnection");
            Data.BaseDataAccess.ConnectionString = connection;
        }

        [HttpGet]
        //[ActionName("GetEmployees")]
        public List<Employee> Employee Get()
        {
            List<Employee> employees = new List<Employee>();
            //Employee employees = null;
            List<DbParameter> parameterList = new List<DbParameter>();
            using (DbDataReader dataReader = baseData.GetDataReader("SelectAll", parameterList, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        //,Salary=@Salary where EmployeeId=@employeeid	
                        Employee employee = new Employee();
                        employee.EmployeeId = (int)dataReader["EmployeeId"];
                        employee.Name = (string)dataReader["Name"];
                        employee.Gender = (string)dataReader["Gender"];
                        employee.Age = (int)dataReader["Age"];
                        employee.Position = (string)dataReader["Position"];
                        employee.Salary = (int)dataReader["Salary"];
                        employees.Add(employee);
                    }
                }
            }
            return employees;
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