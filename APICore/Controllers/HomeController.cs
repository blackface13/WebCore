using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using APICore.CoreBase;
using Microsoft.AspNetCore.Mvc;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APICore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        public IActionResult Index(string id)
        {
            //var a = Path.GetFileNameWithoutExtension("DevExpress.XtraGauges.v19.1.Presets.xml");
            return Ok("BlackFace API");
        }
    }
}
