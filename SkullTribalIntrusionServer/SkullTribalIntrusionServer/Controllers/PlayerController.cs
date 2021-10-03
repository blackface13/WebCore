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
            return Ok("BlackFace API 2");
        }

        [HttpGet("GetById/{playerId}")]
        public async Task<ActionResult<PlayerEntity>> GetById(Guid playerId)
        {
            var result = await _context.Players.FindAsync(playerId);
            return result != null ? result : null;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<PlayerEntity>>> GetAll()
        {
            return await _context.Players.ToListAsync();
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(PlayerEntity p)
        {
            try
            {
                //Chuyển đổi dữ liệu các list để đẩy vào DB
                p.ArrowsBagValues = Systems.ConvertListToSplitString(p.ArrowsBag);
                p.ArrowsBuyedValues = Systems.ConvertListToSplitString(p.ArrowsBuyed);
                //var itemExist2 = _context.PlayerItems.Where(x => x.PlayerId.Equals(p.PlayerId)).ToList();

                //Update/Add player
                var player = _context.Players.FindAsync(p.PlayerId).Result;
                if (player != null)
                    await DbContextExtensions.SingleUpdateAsync(_context, p);
                //_context.Players.Update(p);
                else
                    await DbContextExtensions.SingleInsertAsync(_context, p);
                //await _context.Players.AddAsync(p);

                //Update item
                var itemExist = _context.PlayerItems.Where(x => x.PlayerId == p.PlayerId);
                if (itemExist != null)
                    await DbContextExtensions.BulkDeleteAsync(_context, itemExist);
                //_context.PlayerItems.RemoveRange(itemExist);
                //await _context.PlayerItems.AddRangeAsync(p.ItemsData);
                await DbContextExtensions.BulkInsertAsync(_context, p.ItemsData);

                await DbContextExtensions.BulkSaveChangesAsync(_context);
                return CreatedAtAction("Response", new ResponseModel { Res = Systems.State.Success });
            }
            catch (Exception ex)
            {
                return CreatedAtAction("Response", new ResponseModel { Res = Systems.State.Failed, Messages = ex.Message });
            }
        }

        // GET api/<SyncDataController>/5
        [HttpGet("tank")]
        public async Task<IActionResult> tank()
        {
            return await Update(new PlayerEntity { PlayerId = Guid.Parse("6cb0490f-8d2a-45e1-bccd-033f73bd1e84") });
        }

        // GET api/<SyncDataController>/5
        [HttpGet("createplayer")]
        public async Task<ActionResult<IEnumerable<PlayerEntity>>> CreatePlayerRand()
        {
            var newP = new PlayerEntity()
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

        //Xoá người dùng
        [HttpDelete]
        public bool Delete(PlayerEntity player)
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
