using System;

namespace MyERP.WebClient.Models
{
    public class CustomerDataChangeHistoryViewModel
    {
        public string FieldName { get; set; }

        public string PreviousValue { get; set; }

        public string CurrentValue { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
