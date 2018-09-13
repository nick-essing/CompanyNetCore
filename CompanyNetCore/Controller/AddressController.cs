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
    [Route("api/Address")]
    public class AddressController : ControllerBase
    {
        AddressRepo ar = new AddressRepo();

        [HttpGet]
        public IActionResult Read()
        {
            List<Address> result = ar.Read();
            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }
        [HttpGet("{Id}")]
        public IActionResult Read(int Id)
        {
            Address result = ar.Read(Id);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }
        [HttpPost]
        public IActionResult spInsert([FromBody] Address address)
        {
            Address result = ar.spInsertOrUpdate(address);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }
        [HttpPut]
        public IActionResult spUpdate([FromBody] Address address)
        {
            Address result = ar.spInsertOrUpdate(address);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }
        [HttpDelete]
        public IActionResult spDelete(int Id)
        {
            Address result = ar.spDelete(Id);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}

