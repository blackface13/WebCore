using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIGame.CoreBase;
using APIGame.Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using APIGame.Infrastructure.Models;

namespace APIGame.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlayerController : ControllerCustoms
    {
        private readonly GameDBContext _context;

        public PlayerController(GameDBContext context)
        {
            _context = context;
        }

        // GET: api/Player/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(string id)
        {
            var player = await _context.Player.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        /// <summary>
        /// Đồng bộ thông tin người chơi
        /// </summary>
        [HttpPut("syncplayer")]
        public async Task<IActionResult> SyncPlayer(Player dataString)
        {
            var result = await _context.Player.FindAsync(dataString.UserID);

            if (result == null)
            {
                _context.Add(dataString);
                try
                {
                    await _context.SaveChangesAsync();
                    goto EndPos;
                }
                catch (DbUpdateException)
                {
                    return CreatedAtAction("Response", new ResponseModel { Res = Systems.State.Failed });
                }
            }
            else
            {
                if(result.HWID != dataString.HWID)
                    return CreatedAtAction("Response", new ResponseModel { Res = Systems.State.SyncFailedByHWID });

                result.HWID = dataString.HWID;
                result.NumberSpined = dataString.NumberSpined;
                result.PassCode = dataString.PassCode;
                result.UserName = dataString.UserName;

                _context.Player.Update(result);

                try
                {
                    await _context.SaveChangesAsync();
                    goto EndPos;
                }
                catch (DbUpdateConcurrencyException)
                {
                    return CreatedAtAction("Response", new ResponseModel { Res = Systems.State.Failed });
                }
            }
        EndPos:
            return CreatedAtAction("Response", new ResponseModel { Res = Systems.State.Success });
        }

        /// <summary>
        /// Đồng bộ dữ liệu inventory (Không cho gọi riêng, phải đồng bộ OK thông tin người chơi)
        /// </summary>
        [HttpPut("syncinventory")]
        public async Task<IActionResult> SyncInventory(InventoryEntity dataString)
        {
            var result = await _context.Inventory.FindAsync(dataString.UserID);

            if (result == null)
            {
                _context.Add(dataString);
                try
                {
                    await _context.SaveChangesAsync();
                    goto EndPos;
                }
                catch (DbUpdateException)
                {
                    return CreatedAtAction("Response", new ResponseModel { Res = Systems.State.Failed });
                }
            }
            else
            {
                //Modify dữ liệu
                result.LastUpdate = Systems.GetTimeNowToInteger();
                result.HWID = dataString.HWID;
                result.Data = dataString.Data;

                _context.Inventory.Update(result);

                try
                {
                    await _context.SaveChangesAsync();
                    goto EndPos;
                }
                catch (DbUpdateConcurrencyException)
                {
                    return CreatedAtAction("Response", new ResponseModel { Res = Systems.State.Failed });
                }
            }
        EndPos:
            return CreatedAtAction("Response", new ResponseModel { Res = Systems.State.Success });
        }

        // POST: api/Player
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player player)
        {
            _context.Player.Add(player);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PlayerExists(player.UserID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPlayer", new { id = player.UserID }, player);
        }

        // DELETE: api/Player/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Player>> DeletePlayer(string id)
        {
            var player = await _context.Player.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Player.Remove(player);
            await _context.SaveChangesAsync();

            return player;
        }

        private bool PlayerExists(string id)
        {
            return _context.Player.Any(e => e.UserID == id);
        }
    }
}
