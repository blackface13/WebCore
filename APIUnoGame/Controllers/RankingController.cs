using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIGame.CoreBase;
using APIUnoGame.Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using APIGame.Infrastructure.Models;

namespace APIUnoGame.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RankingController : ControllerBase
    {
        private readonly GameDBContext _context;

        public RankingController(GameDBContext context)
        {
            _context = context;
        }

        // GET: api/Ranking
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ranking>>> GetRanking()
        {
            //return await _context.Ranking.ToListAsync();
            return await _context.Ranking.OrderByDescending(x => x.UnoBasicPoint+ x.UnoExtensionPoint).Take(10).ToListAsync();
        }

        // GET: api/Ranking/5
        [HttpGet("rankingglobal")]
        public async Task<ActionResult<IEnumerable<Ranking>>> GetRankingGlobal()
        {
            return await _context.Ranking.OrderByDescending(x => x.UnoBasicPoint + x.UnoExtensionPoint).Take(10).ToListAsync();
        }

        // GET: api/Ranking/5
        [HttpGet("ranking/{codeCountry}")]
        public async Task<ActionResult<IEnumerable<Ranking>>> GetRankingGlobal(string codeCountry)
        {
            return await _context.Ranking.Where(x => x.Country == codeCountry).OrderByDescending(x => x.UnoBasicPoint + x.UnoExtensionPoint).Take(10).ToListAsync();
        }

        // PUT: api/Ranking/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("putranking")]
        public async Task<IActionResult> PutRanking(Ranking Ranking)
        {
            var result = await _context.Ranking.FindAsync(Ranking.UserID);

            if (result == null)
            {
                _context.Add(Ranking);
                try
                {
                    await _context.SaveChangesAsync();
                    goto EndPos;
                }
                catch (DbUpdateException ex)
                {
                    return CreatedAtAction("Response", new ResponseModel { Res = Systems.State.Failed });
                }
            }
            else
            {
               // result.Country = Ranking. UserID;
                result.Country = Ranking.Country;
                result.UserName = Ranking.UserName;
                result.UnoBasicWinRound = Ranking.UnoBasicWinRound;
                result.UnoBasicLoseRound = Ranking.UnoBasicLoseRound;
                result.UnoBasicPoint = Ranking.UnoBasicPoint;
                result.UnoExtensionWinRound = Ranking.UnoExtensionWinRound;
                result.UnoExtensionLoseRound = Ranking.UnoExtensionLoseRound;
                result.UnoExtensionPoint = Ranking.UnoExtensionPoint;


                _context.Ranking.Update(result);

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

        // POST: api/Ranking
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Ranking>> PostRanking(Ranking Ranking)
        {
            _context.Ranking.Add(Ranking);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RankingExists(Ranking.UserID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRanking", new { id = Ranking.UserID }, Ranking);
        }

        private bool RankingExists(string id)
        {
            return _context.Ranking.Any(e => e.UserID == id);
        }
    }
}
