namespace TelegramShop.Qiwi.QiwiApi.Entities.Profile
{
    using System;

    public class AuthInfo
    {
        public string boundEmail;
        public string ip;
        public DateTime? lastLoginDate;
        public MobilePinInfo mobilePinInfo;
        public PassInfo passInfo;
        public long? personId;
        public PinInfo pinInfo;
        public DateTime? registrationDate;
    }
}