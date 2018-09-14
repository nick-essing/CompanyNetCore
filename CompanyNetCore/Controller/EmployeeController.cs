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
            List<Employee> result;
            try
            {
                result = EmployeeRepo.GetInstance().Read();
            }
            catch (Helper.RepoException<Helper.ResultType> ex)
            {
                switch (ex.Type)
                {
                    case Helper.ResultType.SQLERROR:
                        return StatusCode(StatusCodes.Status409Conflict);
                    default:
                        return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            if (result == null)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }
        [HttpGet("{Id}")]
        public IActionResult Read(int Id)
        {
            Employee result;
            try
            {
                result = EmployeeRepo.GetInstance().Read(Id);
            }
            catch (Helper.RepoException<Helper.ResultType> ex)
            {
                switch (ex.Type)
                {
                    case Helper.ResultType.SQLERROR:
                        return StatusCode(StatusCodes.Status409Conflict);
                    default:
                        return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            if (result == null)
                return StatusCode(StatusCodes.Status204NoContent);
            return StatusCode(StatusCodes.Status200OK, result);

        }
        [HttpPost]
        public IActionResult Create([FromBody] Employee employee)
        {
            Employee result;
            try
            {
                result = EmployeeRepo.GetInstance().Create(employee);
            }
            catch (Helper.RepoException<Helper.ResultType> ex)
            {
                switch (ex.Type)
                {
                    case Helper.ResultType.SQLERROR:
                        return StatusCode(StatusCodes.Status409Conflict);
                    case Helper.ResultType.INVALIDEARGUMENT:
                        return StatusCode(StatusCodes.Status406NotAcceptable);
                    default:
                        return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }
        [HttpPut]
        public IActionResult Update([FromBody] Employee employee)
        {
            Employee result;
            try
            {
                result = EmployeeRepo.GetInstance().Update(employee);
            }
            catch (Helper.RepoException<Helper.ResultType> ex)
            {
                switch (ex.Type)
                {
                    case Helper.ResultType.SQLERROR:
                        return StatusCode(StatusCodes.Status409Conflict);
                    case Helper.ResultType.INVALIDEARGUMENT:
                        return StatusCode(StatusCodes.Status406NotAcceptable);
                    default:
                        return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }
        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            Employee result;
            try
            {
                result = EmployeeRepo.GetInstance().Delete(Id);
            }
            catch (Helper.RepoException<Helper.ResultType> ex)
            {
                switch (ex.Type)
                {
                    case Helper.ResultType.SQLERROR:
                        return StatusCode(StatusCodes.Status409Conflict);
                    case Helper.ResultType.INVALIDEARGUMENT:
                        return StatusCode(StatusCodes.Status406NotAcceptable);
                    default:
                        return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
} 
