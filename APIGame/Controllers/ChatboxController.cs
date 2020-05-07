using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using APIGame.CoreBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIGame.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatboxController : Controller
    {
        // GET: Chatbox
        [AllowAnonymous]
        public IActionResult Index()
        {
            var a = System.IO.File.ReadAllLines(Directory.GetCurrentDirectory() + @"\" + GlobalVariables.ChatboxPathFile);
            return Ok(a[3]);
        }
    }
}