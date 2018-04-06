using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autotrade
{
    class Inventory
    {
        public int id { get; set; }
        public int amount { get; set; }
        public int showcase { get; set; }
        public string color { get; set; }
        public string img { get; set; }
        public string inspect { get; set; }
        public string type { get; set; }
        public string classid { get; set; }
        public string instanceid { get; set; }
        public string market_name { get; set; }
        public string market_hash_name { get; set; }
        public int appid { get; set; }
        public string contextid { get; set; }
        public string assetid { get; set; }
        public int bot_id { get; set; }
        public int state { get; set; }
        public int bumptime { get; set; }
        public string wear { get; set; }
        public int[] stickers { get; set; }
        public int val_1 { get; set; }
        public string market_fee_app { get; set; }
        public string escrow_ends { get; set; }
        public Flags flags { get; set; }
        public int id_parent { get; set; }
        public int added_time { get; set; }
        public string offer_id { get; set; }
        public Boolean offer_untradable { get; set; }
        public Boolean requires_support { get; set; }
        public string can_repair { get; set; }

    }
}
