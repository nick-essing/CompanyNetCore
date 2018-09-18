using System.Collections.Generic;
using CompanyNetCore.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CompanyNetCore.Interfaces;
using CompanyNetCore.Helper;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace CompanyNetCore.Controller
{
    [Route("api/Employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IRepository<Employee> _employeeRepo;

        public EmployeeController(IRepository<Employee> employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }
        [HttpGet]
        public IActionResult Read()
        {
            List<Employee> result;
            try
            {
                result = _employeeRepo.Read();
            }
            catch (Helper.RepoException<ResultType> ex)
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
                result = _employeeRepo.Read(Id);
            }
            catch (Helper.RepoException<ResultType> ex)
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
            if (!Authorization.isAuthorised(Request.Headers["Authorization"].ToString()))
                return StatusCode(StatusCodes.Status401Unauthorized);
            Employee result;
            try
            {
                result = _employeeRepo.Create(employee);
            }
            catch (Helper.RepoException<ResultType> ex)
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
            if (!Authorization.isAuthorised(Request.Headers["Authorization"].ToString()))
                return StatusCode(StatusCodes.Status401Unauthorized);
            Employee result;
            try
            {
                result = _employeeRepo.Update(employee);
            }
            catch (Helper.RepoException<ResultType> ex)
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
            if (!Authorization.isAuthorised(Request.Headers["Authorization"].ToString()))
                return StatusCode(StatusCodes.Status401Unauthorized);
            Employee result;
            try
            {
                result = _employeeRepo.Delete(Id);
            }
            catch (Helper.RepoException<ResultType> ex)
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
