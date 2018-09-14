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
        [HttpGet]
        public IActionResult Read()
        {
            List<Address> result = AddressRepo.GetInstance().Read();
            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }
        [HttpGet("{Id}")]
        public IActionResult Read(int Id)
        {
            Address result = AddressRepo.GetInstance().Read(Id);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }
        [HttpPost]
        public IActionResult Insert([FromBody] Address address)
        {
            Address result = AddressRepo.GetInstance().Create(address);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }
        [HttpPut]
        public IActionResult Update([FromBody] Address address)
        {
            Address result = AddressRepo.GetInstance().Update(address);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }
        [HttpDelete]
        public IActionResult spDelete(int Id)
        {
            Address result = AddressRepo.GetInstance().spDelete(Id);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}

