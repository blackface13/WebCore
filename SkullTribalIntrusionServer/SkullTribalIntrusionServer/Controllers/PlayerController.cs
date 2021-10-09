using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkullTribalIntrusionServer.CoreBase;
using SkullTribalIntrusionServer.Infrastructure.Entities;
using SkullTribalIntrusionServer.Infrastructure.Models;
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
        public async Task<IActionResult> GetById(Guid playerId)
        {
            //Tìm player trong DB
            var resultTemp = await _context.Players.FindAsync(playerId);
            PlayerModel result;
            if (resultTemp != null)
                result = Systems.Mapper.Map<PlayerModel>(resultTemp);
            else
                return CreatedAtAction("Response", new ResponseModel { Res = Systems.State.Success, ErrorCode = 2 });

            //Lấy items của player nếu có
            result.ItemsData = await _context.PlayerItems.Where(x => x.PlayerId == result.PlayerId).ToListAsync();

            //Chuyển đổi dữ liệu mũi tên
            if (resultTemp.ArrowsBagValues != null)
                result.ArrowsBag = Systems.ConvertStringSplitToList<int>(resultTemp.ArrowsBagValues);
            if (resultTemp.ArrowsBuyedValues != null)
                result.ArrowsBuyed = Systems.ConvertStringSplitToList<int>(resultTemp.ArrowsBuyedValues);

            return CreatedAtAction("Response", new ResponseModel { Res = Systems.State.Success, Results = result != null ? result : null });
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<PlayerEntity>>> GetAll()
        {
            return await _context.Players.ToListAsync();
        }

        /// <summary>
        /// Đồng bộ dữ liệu
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [HttpPost("Sync")]
        public async Task<IActionResult> Sync(PlayerModel p)
        {
            try
            {
                PlayerEntity result = Systems.Mapper.Map<PlayerEntity>(p);

                //Chuyển đổi dữ liệu các list để đẩy vào DB
                result.ArrowsBagValues = Systems.ConvertListToSplitString(p.ArrowsBag);
                result.ArrowsBuyedValues = Systems.ConvertListToSplitString(p.ArrowsBuyed);
                result.LastTimeSync = Systems.GetTimeNowToInteger();
                result.HWID = result.HWID.Trim();

                //Kiểm tra tồn tại player
                var player = _context.Players.FindAsync(result.PlayerId).Result;
                if (player != null)//Nếu tồn tại
                {
                    //Check HWID, nếu khác nhau, trả về câu hỏi
                    if (player.HWID.Trim() != p.HWID.Trim() && !p.IsForcedSync)
                        return CreatedAtAction("Response", new ResponseModel { Res = Systems.State.Success, ErrorCode = 3 });

                    //Check thời gian đồng bộ hoá, nếu client cũ hơn => hỏi client xem có get dữ liệu từ server về ko
                    if (player.LastTimeSync > p.LastTimeSync && !p.IsForcedSync)
                        return CreatedAtAction("Response", new ResponseModel { Res = Systems.State.Success, ErrorCode = 1 });

                    await DbContextExtensions.SingleUpdateAsync(_context, result);
                }
                else
                {
                    await DbContextExtensions.SingleInsertAsync(_context, result);
                }

                //Find item
                var itemExist = await _context.PlayerItems.Where(x => x.PlayerId == result.PlayerId).ToListAsync();

                //Xoá item cũ khỏi db
                if (itemExist != null && itemExist.Count > 0)
                    await DbContextExtensions.BulkDeleteAsync(_context, itemExist);

                //Insert item mới
                foreach (var item in result.ItemsData)
                    item.PlayerId = result.PlayerId;
                await DbContextExtensions.BulkInsertAsync(_context, result.ItemsData);

                //Lưu dữ liệu vào DB
                await DbContextExtensions.BulkSaveChangesAsync(_context);
                return CreatedAtAction("Response", new ResponseModel { Res = Systems.State.Success, Messages = result.LastTimeSync.ToString() });//Messages trả về là time đồng bộ hoá, client sẽ nhận và save số này
            }
            catch (Exception ex)
            {
                return CreatedAtAction("Response", new ResponseModel { Res = Systems.State.Failed, Messages = ex.Message });
            }
        }

        /// <summary>
        /// Đồng bộ dữ liệu, client chạy ngầm sẽ gọi hàm này
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [HttpPut("AutoSync")]
        public void AutoSync(PlayerModel p)
        {
            try
            {
                PlayerEntity result = Systems.Mapper.Map<PlayerEntity>(p);

                //Chuyển đổi dữ liệu các list để đẩy vào DB
                result.ArrowsBagValues = Systems.ConvertListToSplitString(p.ArrowsBag);
                result.ArrowsBuyedValues = Systems.ConvertListToSplitString(p.ArrowsBuyed);
                result.LastTimeSync = Systems.GetTimeNowToInteger();
                result.HWID = result.HWID.Trim();

                //Kiểm tra tồn tại player
                var player = _context.Players.FindAsync(result.PlayerId).Result;
                if (player != null)//Nếu tồn tại
                {
                    //Check HWID, nếu khác nhau, ko đồng bộ
                    if (player.HWID.Trim() != p.HWID.Trim())
                        return;

                    DbContextExtensions.SingleUpdate(_context, result);
                }
                else
                {
                    DbContextExtensions.SingleInsert(_context, result);
                }

                //Find item
                var itemExist = _context.PlayerItems.Where(x => x.PlayerId == result.PlayerId).ToList();

                //Xoá item cũ khỏi db
                if (itemExist != null && itemExist.Count > 0)
                    DbContextExtensions.BulkDelete(_context, itemExist);

                //Insert item mới
                foreach (var item in result.ItemsData)
                    item.PlayerId = result.PlayerId;
                DbContextExtensions.BulkInsert(_context, result.ItemsData);

                //Lưu dữ liệu vào DB
                DbContextExtensions.BulkSaveChanges(_context);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Kiểm tra xem lần đồng bộ cuối cùng giữa client và server có khớp không
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private async Task<bool> CheckLastTimeSync([FromBody] PlayerEntity p)
        {

            return await Task.FromResult(false);
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
