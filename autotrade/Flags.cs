using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autotrade
{
    class Flags
    {
        private Boolean tf2_uncraftable;
        private Boolean trade_locked;
        private Boolean has_screenshots;
        private Boolean screenshot_is_video;
        private Boolean steam_commodity;

        public Boolean Tf2_uncraftable
        {
            get
            {
                return tf2_uncraftable;
            }
            set
            {
                this.tf2_uncraftable = value;
            }
        }
        public Boolean Trade_locked
        {
            get
            {
                return trade_locked;
            }
            set
            {
                this.trade_locked = value;
            }
        }
        public Boolean Has_screenshots
        {
            get
            {
                return has_screenshots;
            }
            set
            {
                this.has_screenshots = value;
            }
        }
        public Boolean Screenshot_is_video
        {
            get
            {
                return screenshot_is_video;
            }
            set
            {
                this.screenshot_is_video = value;
            }
        }
        public Boolean Steam_commodity
        {
            get
            {
                return steam_commodity;
            }
            set
            {
                this.steam_commodity = value;
            }
        }
    }
}
