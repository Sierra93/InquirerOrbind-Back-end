using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InquirerOrbind_Back_end.Models {
    /// <summary>
    /// Модель описывает подарки.
    /// </summary>
    public class Prize {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("category", TypeName = "nvarchar(max)")]
        public string Category { get; set; }    // Заголовок подарка.

        [Column("detail", TypeName = "nvarchar(max)")]
        public string Detail { get; set; }  // Описание подарка. 

        [Column("points", TypeName = "int")]
        public int Points { get; set; } // Кол-во баллов необходимых для покупки.

        public List<MultepleContextTable> MultepleContextTables { get; set; }

        public Prize() {
            MultepleContextTables = new List<MultepleContextTable>();
        }
    }
}
