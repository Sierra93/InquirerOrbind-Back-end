using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InquirerOrbind_Back_end.Models {
    /// <summary>
    /// Модель описывает опросы.
    /// </summary>
    public class Question {
        [Key]
        public int Id { get; set; }

        [Column("user_id", TypeName = "int")]
        public int UserId { get; set; }

        [Column("category", TypeName = "nvarchar(500)")]
        public string Category { get; set; }  // Категория опроса.

        [Column("second_title", TypeName = "nvarchar(500)")]
        public string Title { get; set; } // Заголовок опроса.

        [Column("details", TypeName = "nvarchar(max)")]
        public string Details { get; set; } // Текст опроса.

        [Column("points", TypeName = "int")]
        public int Points { get; set; } // Количество баллов.

        public List<MultepleContextTable> MultepleContextTables { get; set; }

        public Question() {
            MultepleContextTables = new List<MultepleContextTable>();
        }
    }
}
