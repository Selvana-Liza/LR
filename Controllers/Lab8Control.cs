using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using to.Models;

namespace to.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Lab8Controller : ControllerBase
    {
        [HttpGet]
        public IActionResult  Get()
        {
            string Sozdateli = "Создатели: Петрова Е.И., Шевченко М.О.";
            DateTime DT = DateTime.Now;
            string DT1 = DT.ToString();
            return Content(Sozdateli + "   " + DT1);
        }
    }
}
