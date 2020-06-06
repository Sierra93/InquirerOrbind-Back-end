using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InquirerOrbind_Back_end.Models {
    /// <summary>
    /// Модель описывает вывод опроса.
    /// </summary>
    [Table("Questions")]
    public class Question {
        [Key]
        public int Id { get; set; }

        [Column("user_id", TypeName = "int")]
        public int UserId { get; set; } // Id пользователя.

        [Column("question_id", TypeName = "int")]
        public int? QuestionId { get; set; } 

        [Column("category", TypeName = "nvarchar(500)")]
        public string Category { get; set; }  // Категория опроса.

        [Column("second_title", TypeName = "nvarchar(500)")]
        public string Title { get; set; } // Заголовок опроса.

        [Column("details", TypeName = "nvarchar(max)")]
        public string Details { get; set; } // Текст опроса.

        [Column("count_like", TypeName = "int")]
        public int CountLike { get; set; }  // Кол-во лайков опроса.

        [Column("accept_answer", TypeName = "nvarchar(max)")]
        public string AcceptAnswer { get; set; }    // Принятый ответ на опрос.

        [Column("answer_1", TypeName = "nvarchar(max)")]
        public string Answer_1 { get; set; }

        [Column("answer_2", TypeName = "nvarchar(max)")]
        public string Answer_2 { get; set; }

        [Column("answer_3", TypeName = "nvarchar(max)")]
        public string Answer_3 { get; set; }

        [Column("answer_4", TypeName = "nvarchar(max)")]
        public string Asnwer_4 { get; set; }

        [Column("answer_5", TypeName = "nvarchar(max)")]
        public string Answer_5 { get; set; } 

        public List<MultepleContextTable> MultepleContextTables { get; set; }

        public Question() {
            MultepleContextTables = new List<MultepleContextTable>();
        }
    }
}
