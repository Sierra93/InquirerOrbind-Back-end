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
            await db.Questions.AddRangeAsync(question);

            return Ok();
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
    }
}
