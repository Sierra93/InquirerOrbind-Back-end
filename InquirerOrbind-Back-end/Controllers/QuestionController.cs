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
    /// Контроллер описывает работу с опросами.
    /// </summary>
    [ApiController, Route("api/data/question")]
    public class QuestionController : Controller {
        ApplicationDbContext db;

        public QuestionController(ApplicationDbContext _context) {
            db = _context;
        }

        /// <summary>
        /// Метод создает опрос.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost, Route("create")]
        public async Task<IActionResult> CreateQuestion([FromBody] Question question) {
            if (question.UserId == 0) {
                throw new ArgumentNullException("Id пользователя не передан.");
            }

            // Ищет пользователя по его Id.
            var oUser = await db.Users.Where(u => u.Id == question.UserId).FirstOrDefaultAsync();
            var oUserDetail = await db.UserDetails.Where(u => u.Login.Equals(oUser.Login)).FirstOrDefaultAsync();

            // Раз опрос создан, то нужно добавить + 10 баллов тому, кто его создал.
            oUserDetail.Points += 10;
            db.UpdateRange(oUserDetail);
            await db.SaveChangesAsync();

            await db.Questions.AddRangeAsync(question);
            await db.SaveChangesAsync();
                        
            return Ok("Опрос успешно создан.");
        }

        /// <summary>
        /// Метод выбирает опросы, созданные пользователем.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("get-question")]
        public async Task<IActionResult> TakeQuestion([FromBody] Question question) {
            var oQuestion = await db.Questions.Where(q => q.UserId == question.UserId).ToListAsync();

            return Ok(oQuestion);
        }

        /// <summary>
        /// Метод добавляет лайк опросу.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("add-like")]
        public async Task<ActionResult> AddLike([FromBody] Question question) {
            var oQuestion = await db.Questions.Where(q => q.Id == question.Id).FirstOrDefaultAsync();
            oQuestion.CountLike++;

            db.UpdateRange(oQuestion);
            await db.SaveChangesAsync();

            return Ok("Лайк успешно проставлен.");
        }
    }
}
