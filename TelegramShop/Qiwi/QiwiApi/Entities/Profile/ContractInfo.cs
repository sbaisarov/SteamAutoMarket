namespace TelegramShop.Qiwi.QiwiApi.Entities.Profile
{
    using System;
    using System.Collections.Generic;

    public class ContractInfo
    {
        public bool? blocked;
        public long? contactId;
        public DateTime? creationDate;
        public List<object> features;
        public List<IdentificationInfo> IdentificationInfo;
    }
}