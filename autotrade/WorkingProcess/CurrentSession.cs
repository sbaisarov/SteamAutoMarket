﻿using autotrade.Steam;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autotrade.WorkingProcess {
    class CurrentSession {
        public static SteamManager CurrentUser { get; set; }
        public static Image AccountImage { get; set; }
    }
}
