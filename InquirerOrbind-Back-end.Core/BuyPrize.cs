using System;
using System.Collections.Generic;
using System.Text;

namespace InquirerOrbind_Back_end.Core {
    /// <summary>
    /// Модель описывает покупку подарка или бонуса.
    /// </summary>
    public class BuyPrize {
        public int Id { get; set; }

        public int PrizeId { get; set; }    // Id подарка или бонуса, на который пользователь меняет баллы.
    }
}
