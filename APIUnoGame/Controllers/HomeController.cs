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
using APIGame.Infrastructure.Entities;

namespace APIGame.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly GameDBContext _context;

        // private readonly IHostingEnvironment Hosting;
        public HomeController(GameDBContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        //[Authorize]
        public IActionResult Index(string id)
        {
            return Ok("BlackFace API");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> TankTest(string id)
        {
            Player test = new Player { UserID = "fd4f"};

            var tank = await _context.Player.FindAsync(test.UserID);
            if(tank == null)
            {
                _context.Player.Add(test);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                }
            }
            return tank;
        }
    }
}
