using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Xml.Linq;

namespace InquirerOrbind_Back_end.Models {
    /// <summary>
    /// Модель описывает детальную информацию о пользователе.
    /// </summary>
    [Table("UserDetails")]
    public class UserDetail {
        [Key]
        public int Id { get; set; }

        [Column("login", TypeName = "nvarchar(500)")]
        public string Login { get; set; }   // Логин.

        [Column("avatar", TypeName = "nvarchar(max)")]
        public string Avatar { get; set; }  // Аватар.

        [Column("first_name", TypeName = "nvarchar(500)")]
        public string FirstName { get; set; }   // Имя.

        [Column("middle_name", TypeName = "nvarchar(max)")]
        public string MiddleName { get; set; }  // Отчество.

        [Column("age", TypeName = "int")]
        public int? Age { get; set; }    // Возраст.

        [Column("floor", TypeName = "nvarchar(10)")]
        public string Floor { get; set; } 

        [Column("last_name", TypeName = "nvarchar(500)")]
        public string LastName { get; set; }    // Фамилия.

        [Column("email", TypeName = "nvarchar(500)")]
        public string Email { get; set; }

        [Column("points", TypeName = "int")]
        public int Points { get; set; }

        public List<MultepleContextTable> MultepleContextTables { get; set; }

        public UserDetail() {
            MultepleContextTables = new List<MultepleContextTable>();
        }
    }
}
