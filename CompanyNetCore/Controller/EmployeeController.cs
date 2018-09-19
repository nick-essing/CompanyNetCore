using System.Collections.Generic;
using CompanyNetCore.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CompanyNetCore.Interfaces;
using CompanyNetCore.Helper;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.AspNetCore.Http.Internal;
using TobitLogger.Core.Models;
using System;
using Microsoft.Extensions.Logging;
using TobitLogger.Core;

namespace CompanyNetCore.Controller
{
    [Route("api/Employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IRepository<Employee> _employeeRepo;

        public EmployeeController(IRepository<Employee> employeeRepo, ILoggerFactory loggerFactory)
        {
            _employeeRepo = employeeRepo;
            _logger = loggerFactory.CreateLogger<EmployeeController>();
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
                var logObj = new ExceptionData(ex);
                logObj.CustomText = "Insert";
                logObj.Add("start_time", DateTime.UtcNow);

                logObj.Add("Employee", employee);
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
                var logObj = new ExceptionData(ex);
                logObj.CustomNumber = employee.Id;
                logObj.CustomText = "Update";
                logObj.Add("start_time", DateTime.UtcNow);

                logObj.Add("Employee", employee);
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
            catch (RepoException<ResultType> ex)
            {
                var logObj = new ExceptionData(ex);
                logObj.CustomNumber = Id;
                logObj.CustomText = "Delete";
                logObj.Add("start_time", DateTime.UtcNow);

                logObj.Add("Id", Id);
                _logger.Error(logObj);
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
