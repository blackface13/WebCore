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

namespace APIGame.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HourRewardController : ControllerBase
    {
        private readonly GameDBContext _context;

        public HourRewardController(GameDBContext context)
        {
            _context = context;
        }

        // GET: api/HourReward
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HourRewardEntity>>> GetHourReward()
        {
            return await _context.HourReward.ToListAsync();
        }

        // GET: api/HourReward/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HourRewardEntity>> GetHourRewardEntity(string id)
        {
            var hourRewardEntity = await _context.HourReward.FindAsync(id);

            if (hourRewardEntity == null)
            {
                return NotFound();
            }

            return hourRewardEntity;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHourRewardEntity(string id, HourRewardEntity hourRewardEntity)
        {
            if (id != hourRewardEntity.UserID)
            {
                return BadRequest();
            }

            _context.Entry(hourRewardEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HourRewardEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<HourRewardEntity>> PostHourRewardEntity(HourRewardEntity hourRewardEntity)
        {
            _context.HourReward.Add(hourRewardEntity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HourRewardEntityExists(hourRewardEntity.UserID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHourRewardEntity", new { id = hourRewardEntity.UserID }, hourRewardEntity);
        }

        // DELETE: api/HourReward/5
        //[HttpGet("del/{id}")]
        //public async Task<ActionResult<HourRewardEntity>> DeleteHourRewardEntity(string id)
        //{
        //    var hourRewardEntity = await _context.HourReward.FindAsync(id);
        //    if (hourRewardEntity == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.HourReward.Remove(hourRewardEntity);
        //    await _context.SaveChangesAsync();

        //    return hourRewardEntity;
        //}

        private bool HourRewardEntityExists(string id)
        {
            return _context.HourReward.Any(e => e.UserID == id);
        }
    }
}
