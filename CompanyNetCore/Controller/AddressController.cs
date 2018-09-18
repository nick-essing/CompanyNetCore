using System.Collections.Generic;
using CompanyNetCore.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CompanyNetCore.Interfaces;
using CompanyNetCore.Helper;

namespace CompanyNetCore.Controller
{
    [Route("api/Address")]
    public class AddressController : ControllerBase
    {
        private readonly IRepository<Address> _addressRepo;

        public AddressController(IRepository<Address> addressRepo)
        {
            _addressRepo = addressRepo;
        }
        [HttpGet]
        public IActionResult Read()
        {
            List<Address> result;
            try
            {
                result = _addressRepo.Read();
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
            Address result;
            try
            {
                result = _addressRepo.Read(Id);
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
        public IActionResult Create([FromBody] Address address)
        {
            if (!Authorization.isAuthorised(Request.Headers["Authorization"].ToString()))
                return StatusCode(StatusCodes.Status401Unauthorized);
            Address result;
            try
            {
                result = _addressRepo.Create(address);
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
        public IActionResult Update([FromBody] Address address)
        {
            if (!Authorization.isAuthorised(Request.Headers["Authorization"].ToString()))
                return StatusCode(StatusCodes.Status401Unauthorized);
            Address result;
            try
            {
                result = _addressRepo.Update(address);
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
            if (!Authorization.isAuthorised(Request.Headers["Authorization"].ToString()))
                return StatusCode(StatusCodes.Status401Unauthorized);
            Address result;
            try
            {
                result = _addressRepo.Delete(Id);
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

