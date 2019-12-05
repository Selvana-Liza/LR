using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using to.Models;
using to.Storage;
using Serilog;

namespace to.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabController : ControllerBase
    {
        private static IStorage<Lab1Mod> _memCache = new MemCache();
 
        [HttpGet]
        public ActionResult<IEnumerable<Lab1Mod>> Get()
        {
            Log.Information("Get record");
            return Ok(_memCache.All);
        }
    
        [HttpGet("{id}")]
        public ActionResult<Lab1Mod> Get(Guid id)
        {
            if (!_memCache.Has(id)) 
            return NotFound("No such");
            Log.Information("Get record");
            return Ok(_memCache[id]);
     }
    
        [HttpPost]
        public IActionResult Post([FromBody] Lab1Mod value)
        {
            var validationResult = value.Validate();
            if (!validationResult.IsValid) return
            BadRequest(validationResult.Errors);
            _memCache.Add(value);
            Log.Information("Post record");
            return Ok($"{value.ToString()} has been added");
        }
    
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Lab1Mod value)
        {
            if (!_memCache.Has(id)) return NotFound("No such");
            var validationResult = value.Validate();
            if (!validationResult.IsValid) return
            BadRequest(validationResult.Errors);
            var previousValue = _memCache[id];
            _memCache[id] = value;
            Log.Information("Put record");
            return Ok($"{previousValue.ToString()} has been updated to {value.ToString()}");
        }
    
        [HttpDelete("{id}")]
            public IActionResult Delete(Guid id)
            {
            if (!_memCache.Has(id)) return NotFound("No such");
            var valueToRemove = _memCache[id];
            _memCache.RemoveAt(id);
            Log.Information("Delete record");
            return Ok($"{valueToRemove.ToString()} has been removed");
        }
    }
}
