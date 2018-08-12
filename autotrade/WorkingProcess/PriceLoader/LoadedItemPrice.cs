using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autotrade.WorkingProcess.PriceLoader {
    class LoadedItemPrice {
        public DateTime ParseTime { get; set; }
        public double Price { get; set; }

        public LoadedItemPrice(DateTime parseTime, double price) {
            ParseTime = parseTime;
            Price = price;
        }
    }
}
