using System;
using System.Collections.Generic;
using System.Text;

namespace InquirerOrbind_Back_end.Models {
    public class MultepleContextTable {
        public int UserId { get; set; }

        public User User { get; set; }

        public int DetailId { get; set; }
         
        public UserDetail DetailUser { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }
    }
}
