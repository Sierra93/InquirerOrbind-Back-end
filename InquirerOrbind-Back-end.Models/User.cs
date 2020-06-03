using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InquirerOrbind_Back_end.Models {
    /// <summary>
    /// Модель описывает пользователя.
    /// </summary>
    [Table("Users")]
    public class User {
        [Key, Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указан логин."), Column("login")]
        public string Login { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес.")]
        [Required(ErrorMessage = "Не указана почта."), Column("email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан номер телефона."), Column("number")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Не указан пароль."), Column("password")]
        public string Password { get; set; }

        public List<MultepleContextTable> MultepleContextTables { get; set; }

        public User() {
            MultepleContextTables = new List<MultepleContextTable>();
        }
    }
}
