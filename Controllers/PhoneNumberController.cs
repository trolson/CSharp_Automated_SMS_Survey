﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SMS_Example_Survey.Controllers
{
    [Route("[controller]")]
    public class PhoneNumberController : Controller
    {
        // Use Bandwidth.Net.Api interfaces via DI here if need

        [HttpGet]
        public IActionResult Get()
        {
            //FinishStartup.NUMBER = HttpContext.Items["PhoneNumber"].ToString();
            return Json(new { PhoneNumber = HttpContext.Items["PhoneNumber"] });
        }

    }
}
