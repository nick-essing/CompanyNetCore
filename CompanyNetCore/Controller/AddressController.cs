using System.Collections.Generic;
using CompanyNetCore.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CompanyNetCore.Interfaces;
using CompanyNetCore.Helper;
using TobitLogger.Core.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using TobitWebApiExtensions.Extensions;

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
        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Create([FromBody] Address address)
        {
            var _user = HttpContext.GetTokenPayload<Auth.Models.LocationUserTokenPayload>();
            var groups = HttpContext.GetUacGroups();
            if (_user.SiteId == "77890-29730")
            {
                Address result;
                try
                {
                    result = _addressRepo.Create(address);
                }
                catch (Helper.RepoException<ResultType> ex)
                {

                    var logObj = new ExceptionData(ex);
                    logObj.CustomText = "Insert";
                    logObj.Add("start_time", DateTime.UtcNow);

                    logObj.Add("Address", address);
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
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }
        [Authorize(Roles = "1")]
        [HttpPut]
        public IActionResult Update([FromBody] Address address)
        {
            var _user = HttpContext.GetTokenPayload<Auth.Models.LocationUserTokenPayload>();
            var groups = HttpContext.GetUacGroups();
            if (_user.SiteId == "77890-29730")
            {
                Address result;
                try
                {
                    result = _addressRepo.Update(address);
                }
                catch (Helper.RepoException<Helper.ResultType> ex)
                {
                    var logObj = new ExceptionData(ex);
                    logObj.CustomNumber = address.Id;
                    logObj.CustomText = "Update";
                    logObj.Add("start_time", DateTime.UtcNow);

                    logObj.Add("Address", address);
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
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }
        [Authorize(Roles = "1")]
        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            var _user = HttpContext.GetTokenPayload<Auth.Models.LocationUserTokenPayload>();
            var groups = HttpContext.GetUacGroups();
            if (_user.SiteId == "77890-29730")
            {
                Address result;
                try
                {
                    result = _addressRepo.Delete(Id);
                }
                catch (Helper.RepoException<ResultType> ex)
                {
                    var logObj = new ExceptionData(ex);
                    logObj.CustomNumber = Id;
                    logObj.CustomText = "Delete";
                    logObj.Add("start_time", DateTime.UtcNow);

                    logObj.Add("Id", Id);
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
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }
    }
}


