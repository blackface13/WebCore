using APIGame.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGame.CoreBase
{
    public class ControllerCustoms : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public new ActionResult<ResponseModel> Response(ResponseModel res)
        {
            return res;
        }
    }
}
