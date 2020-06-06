﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InquirerOrbind_Back_end.Data;
using InquirerOrbind_Back_end.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InquirerOrbind_Back_end.Controllers {
    /// <summary>
    /// Контроллер описывает работу с подарками и бонусами.
    /// </summary>
    [ApiController, Route("api/data/shop")]
    public class PrizeController : Controller {
        ApplicationDbContext db;

        public PrizeController(ApplicationDbContext _context) {
            db = _context;
        }

        /// <summary>
        /// Метод выбирает список всех подарков и бонусов.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("get-prize")]
        public async Task<IActionResult> TakePrize([FromBody] Prize prize) {
            var oPrizes = await db.Prizes.ToListAsync();

            return Ok(oPrizes);
        }

        /// <summary>
        /// Метод совершает выбор подарка или бонуса, при этом тратятся баллы.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("buy")]
        public async Task<IActionResult> Buy([FromBody] UserSignIn user) {
            //var oUser = await db

            return Ok();    // Вернуть купленный приз. через {}
        }
    }
}
