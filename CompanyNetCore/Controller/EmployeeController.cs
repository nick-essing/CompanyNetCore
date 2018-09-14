using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CompanyNetCore.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Dapper;
using CompanyNetCore.Repositories;

namespace CompanyNetCore.Controller
{
    [Route("api/Employee")]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Read()
        {
            List<Employee> result = EmployeeRepo.GetInstance().Read();
            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }
        [HttpGet("{Id}")]
        public IActionResult Read(int Id)
        {
            Employee result = EmployeeRepo.GetInstance().Read(Id);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }
        [HttpPost]
        public IActionResult spInsert([FromBody] Employee employee)
        {
            Employee result = EmployeeRepo.GetInstance().spInsertOrUpdate(employee);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }
        [HttpPut]
        public IActionResult spUpdate([FromBody] Employee employee)
        {
            Employee result = EmployeeRepo.GetInstance().spInsertOrUpdate(employee);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }
        [HttpDelete]
        public IActionResult spDelete(int Id)
        {
            Employee result = EmployeeRepo.GetInstance().spDelete(Id);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}

