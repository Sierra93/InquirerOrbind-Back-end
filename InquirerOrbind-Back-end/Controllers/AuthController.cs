using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;
using InquirerOrbind_Back_end.Data;
using InquirerOrbind_Back_end.Models;
using InquirerOrbind_Back_end.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace InquirerOrbind_Back_end.Controllers {
    /// <summary>
    /// Контроллер регистрации и авторизации пользователя.
    /// </summary>
    [ApiController, Route("api/data/auth")]
    public class AuthController : Controller {
        ApplicationDbContext db;
        public AuthController(ApplicationDbContext _context) {
            db = _context;
        }

        /// <summary>
        /// Метод регистрирует пользователя.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Статус регистрации.</returns>
        [HttpPost, Route("checkin")]
        public async Task<IActionResult> CheckIn([FromBody] User user) {
            if (string.IsNullOrEmpty(user.Login) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Email)) {
                throw new ArgumentNullException("Параметры не должны быть пустыми.");
            }

            // Проверяет, есть ли пользователь с таким логином.
            await GetIdentityLogin(user.Login);

            // Проверяет, есть ли пользователь с таким email.
            await GetIdentityEmail(user.Email);

            string sPassword = user.Password;

            // Хэширует пароль в MD5.
            var hashPassword = await HashMD5Service.HashPassword(sPassword);
            user.Password = hashPassword;

            // Добавляет нового пользователя в БД.
            await db.Users.AddRangeAsync(user);
            await db.SaveChangesAsync();

            return Ok("Пользователь успешно зарегистрирован.");
        }

        /// <summary>
        /// Метод проверяет существование пользователя в БД
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost, Route("signin")]
        public async Task<IActionResult> SignIn([FromBody] UserSignIn user) {
            User userobj = new User();
            if (user.LoginOrEmail == null || user.Password == null) {
                throw new ArgumentNullException("Параметры авторизации не заполнены.");
            }

            // Хэширует пароль для сравнения зашифрованных паролей
            string checkHashString = await HashMD5Service.HashPassword(user.Password);

            // Сравнивает хэши
            var isEqual = await EqualsHash(user.LoginOrEmail);

            if (isEqual != checkHashString) {
                throw new ArgumentException("Не верный пароль.");
            }

            // Проверяет, есть ли пользователь в БД
            var identity = await GetIdentity(user.LoginOrEmail, isEqual);

            // Если пользователь найден, то получаем его ID 
            if (identity != null) {
                bool isEmail = identity.Name.Contains("@"); // Проверяет логин передан или email.

                if (isEmail) {
                    userobj = await db.Users.FirstOrDefaultAsync(u => u.Email == user.LoginOrEmail);
                }
                else {
                    userobj = await db.Users.FirstOrDefaultAsync(u => u.Login == user.LoginOrEmail);
                }
            }

            var now = DateTime.UtcNow;
            if (identity == null) { throw new ArgumentException("У пользователя отсутствует токен доступа."); }

            // Создание JWT-токена
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // Объект анонимного типа с токеном, который отсылается на фронт
            var response = new {
                access_token = encodedJwt,
                user_data = userobj
            };

            return Json(response);
        }

        /// <summary>
        /// Метод выбирает пользователя из БД
        /// </summary>
        /// <param name = "input" ></ param >
        /// < param name="password"></param>
        /// <returns></returns>
        async Task<ClaimsIdentity> GetIdentity(string input, string password) {
            bool isEmail = input.Contains("@"); // Проверяет логин передан или email.

            if (isEmail) {
                var oUser = await db.Users.Where(u => u.Login == input).FirstOrDefaultAsync();
                var claims = new List<Claim> {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, input)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            else {
                var oUser = await db.Users.Where(u => u.Login == input).FirstOrDefaultAsync();
                var claims = new List<Claim> {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, input)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
        }

        /// <summary>
        /// Метод проверяет, существует ли пользователь с таким логином.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        async Task<IActionResult> GetIdentityLogin(string login) {
            if (string.IsNullOrEmpty(login)) {
                throw new ArgumentNullException("Логин не передан.");
            }

            // Проверяет, есть ли пользователь с таким логином.
            var isUser = await db.Users.Where(u => u.Login == login).FirstOrDefaultAsync();

            if (isUser == null) {
                return Ok();
            }

            throw new ArgumentException("Пользователь с таким логином уже существует.");
        }

        /// <summary>
        /// Метод проверяет, существует ли пользователь с таким email.
        /// </summary>
        /// <returns></returns>
        async Task<IActionResult> GetIdentityEmail(string email) {
            if (string.IsNullOrEmpty(email)) {
                throw new ArgumentNullException("Email не передан.");
            }

            // Проверяет, есть ли уже такой email.
            var isEmail = await db.Users.Where(e => e.Email == email).FirstOrDefaultAsync();

            if (isEmail == null) {
                return Ok();
            }

            throw new ArgumentException("Такой email уже существует.");
        }

        /// <summary>
        /// Метод сравнивает пароли при авторизации хэшируя исходный с хэшем в БД.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        async Task<string> EqualsHash(string login) {
            string hashDb = "";
            bool isEmail = login.Contains("@"); // Проверяет логин передан или email.

            if (isEmail) {
                var getHashPassword = await (from u in db.Users
                                             where u.Email == login
                                             select u.Password).ToListAsync();

                foreach (var el in getHashPassword) {
                    hashDb = el;
                }
            }
            else {
                var getHashPassword = await (from u in db.Users
                                             where u.Login == login
                                             select u.Password).ToListAsync();

                foreach (var el in getHashPassword) {
                    hashDb = el;
                }
            }

            return hashDb;
        }

        /// <summary>
        /// Метод получает общее количество зарегистрированных пользователей.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("get-users")]
        public async Task<IActionResult> GetUsers() {
            var aUsers = await db.Users.ToListAsync();

            return Ok(aUsers.Count);
        }
    }
}
