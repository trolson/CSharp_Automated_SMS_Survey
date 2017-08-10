using Microsoft.AspNetCore.Mvc;

namespace SMS_Example_Survey.Controllers
{
    [Route("[controller]")]
    public class PhoneNumberController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { PhoneNumber = HttpContext.Items["PhoneNumber"] });
        }

    }
}
