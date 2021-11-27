using System;

namespace MyERP.Core.Models.Resources
{
    public class CustomerDataChangeHistoryModel
    {
        public string FieldName { get; set; }

        public string PreviousValue { get; set; }

        public string CurrentValue { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
