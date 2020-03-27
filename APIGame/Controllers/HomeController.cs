using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using APIGame.CoreBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
namespace APIGame.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly GameDBContext GData;
       // private readonly IHostingEnvironment Hosting;
        public HomeController(GameDBContext context)
        {
            GData = context;
        }

        [AllowAnonymous]
        public IActionResult Index(string id)
        {
            var a = GData.BreakBall.AsNoTracking();
            var b = a.Count();
            return Ok(Security.Encrypt("Black Face"));
        }
    }
}
