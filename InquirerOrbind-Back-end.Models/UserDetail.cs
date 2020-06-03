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
    public class UserDetail {
        [Key]
        public int Id { get; set; }

        [Column("login", TypeName = "nvarchar(500)")]
        public string Login { get; set; }

        [Column("avatar", TypeName = "nvarchar(max)")]
        public string Avatar { get; set; }

        [Column("first_name", TypeName = "nvarchar(500)")]
        public string FirstName { get; set; }

        [Column("last_name", TypeName = "nvarchar(500)")]
        public string LastName { get; set; } 

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
