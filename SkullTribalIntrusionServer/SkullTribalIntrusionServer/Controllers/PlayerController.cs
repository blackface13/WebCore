using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkullTribalIntrusionServer.CoreBase;
using SkullTribalIntrusionServer.Infrastructure.Entities;
using SkullTribalIntrusionServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkullTribalIntrusionServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : Controller
    {
        private readonly GameDBContext _context;

        public PlayerController(GameDBContext context)
        {
            _context = context;
        }

        // GET: api/<SyncDataController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("BlackFace API");
        }

        [HttpGet("GetById/{playerId}")]
        public async Task<ActionResult<Players>> GetById(Guid playerId)
        {
            return await _context.Players.FindAsync(playerId);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Players>>> GetAll()
        {
            return await _context.Players.ToListAsync();
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(Players p)
        {
            try
            {
                _context.Players.Update(p);
                await _context.SaveChangesAsync();
                return CreatedAtAction("Response", new ResponseModel { Res = Systems.State.Success });
            }
            catch
            {
                return CreatedAtAction("Response", new ResponseModel { Res = Systems.State.Failed });
            }
        }

        // GET api/<SyncDataController>/5
        [HttpGet("tank")]
        public IActionResult tank()
        {
            CreatedAtAction("Update", new Players { PlayerId = Guid.Parse("6cb0490f-8d2a-45e1-bccd-033f73bd1e84") });
            return Ok("OK");
        }

        // GET api/<SyncDataController>/5
        [HttpGet("createplayer")]
        public async Task<ActionResult<IEnumerable<Players>>> CreatePlayerRand()
        {
            var newP = new Players()
            {
                PlayerId = Guid.NewGuid(),
                PlayerName = RandomString(10),
            };
            _context.Players.Add(newP);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Response", new ResponseModel { Res = Systems.State.Success });
        }

        private static Random random = new Random();
        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // POST api/<SyncDataController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SyncDataController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        //Xoá toàn bộ dữ liệu người dùng
        [HttpDelete]
        public bool Delete(Players player)
        {
            try
            {
                _context.Players.Remove(player);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
