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
    /// Контроллер описывает работу с личным кабинетом пользователя.
    /// </summary>
    [ApiController, Route("api/data/front-office")]
    public class FrontOfficeController : Controller {
        ApplicationDbContext db;

        public FrontOfficeController(ApplicationDbContext _context) {
            db = _context;
        }

        /// <summary>
        /// Метод выбирает детальную информацию о пользователе.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Объект с данными пользователя.</returns>
        [HttpPost, Route("get-details")]
        public async Task<IActionResult> TakeDetails([FromBody] UserSignIn user) {
            var oUser = await db.UserDetails.Where(u => u.Login.Equals(user.LoginOrEmail)).FirstOrDefaultAsync(); 

            return Ok(oUser);
        }

        /// <summary>
        /// Метод добавляет детальную информацию о пользователе.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("add-details")]
        public async Task<IActionResult> AddDetails([FromBody] UserDetail user) {
            var oUser = await db.UserDetails.Where(u => u.Id == user.Id).FirstOrDefaultAsync();

            db.UserDetails.UpdateRange(oUser);

            return Ok();
        }
    }
}
