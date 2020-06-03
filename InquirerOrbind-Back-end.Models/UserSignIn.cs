using System;
using System.Collections.Generic;
using System.Text;

namespace InquirerOrbind_Back_end.Models {
    /// <summary>
    /// Модель для идентификации пользователя.
    /// </summary>
    public class UserSignIn {
        public int Id { get; set; }

        public string LoginOrEmail { get; set; }

        public string Password { get; set; }
    }
}
