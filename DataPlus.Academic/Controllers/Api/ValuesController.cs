﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataPlus.Contracts.Logger;
using Microsoft.AspNetCore.Mvc;

namespace DataPlus.Academic.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private ILoggerManager _logger;

        public ValuesController(ILoggerManager logger)
        {
            _logger = logger;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            _logger.LogInfo("Values::Get::Here is info message from our values controller.");
            _logger.LogDebug("Values::Get::Here is debug message from our values controller.");
            _logger.LogWarning("Values::Get::Here is warn message from our values controller.");
            _logger.LogError("Values::Get::Here is error message from our values controller.");
            
            return new string[] { "value1", "value2" };
        }

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
