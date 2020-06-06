using System;
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
                        
            return Ok("Опрос успешно создан. Начислено 10 баллов.");
        }

        /// <summary>
        /// Метод выбирает опросы, созданные пользователем.
        /// </summary>
        /// <returns>Список опросов.</returns>
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
        public async Task<IActionResult> AddLike([FromBody] Question question) {
            var oQuestion = await db.Questions.Where(q => q.Id == question.Id).FirstOrDefaultAsync();
            oQuestion.CountLike++;
            db.UpdateRange(oQuestion);
            await db.SaveChangesAsync();

            // Ищет пользователя по его Id.
            var oUser = await db.Users.Where(u => u.Id == question.UserId).FirstOrDefaultAsync();
            var oUserDetail = await db.UserDetails.Where(u => u.Login.Equals(oUser.Login)).FirstOrDefaultAsync();

            // Раз лайк проставлен, то нужно добавить + 1 балл тому, кто его проставил.
            oUserDetail.Points++;
            db.UpdateRange(oUserDetail);
            await db.SaveChangesAsync();

            return Ok("Лайк успешно проставлен. Начислен 1 балл.");
        }

        /// <summary>
        /// Метод выбирает все опросы, кроме тех, которые создал пользователь.
        /// </summary>
        /// <returns>Список опросов.</returns>
        [HttpPost, Route("get-all-question")]
        public async Task<IActionResult> TakeAllQuestion() {
            var oQuestions = await db.Questions.ToListAsync();

            return Ok(oQuestions);
        }

        /// <summary>
        /// Метод выбирает конкретный опрос (не обязательно свой).
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("get-concrete-question")]
        public async Task<IActionResult> TakeConcretele([FromBody] Question question) {
            var oQuestions = await db.Questions.Where(q => q.Id == question.Id).FirstOrDefaultAsync();

            return Ok(oQuestions);
        }

        /// <summary>
        /// Метод добавляет ответ к опросу.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        [HttpPost, Route("add-answer")]
        public async Task<IActionResult> AddAnswer([FromBody] Question question) {
            // Выбирает опрос, на который отвечает пользователь.
            var oUser = await db.Questions.Where(q => q.Id == question.QuestionId).FirstOrDefaultAsync();
            oUser.AcceptAnswer = question.AcceptAnswer;
            db.UpdateRange(oUser);
            await db.SaveChangesAsync();

            return Ok("Ответ на опрос успешно добавлен.");
        }
    }
}
