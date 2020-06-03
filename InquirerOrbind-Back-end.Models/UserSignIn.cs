using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InquirerOrbind_Back_end.Models {
    /// <summary>
    /// Модель для идентификации пользователя.
    /// </summary>
    public class UserSignIn {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указан логин или почта.")]
        public string LoginOrEmail { get; set; }

        [Required(ErrorMessage = "Не указан пароль.")]
        public string Password { get; set; }
    }
}
