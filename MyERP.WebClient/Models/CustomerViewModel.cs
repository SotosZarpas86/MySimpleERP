using System.Collections.Generic;

namespace MyERP.WebClient.Models
{
    public class CustomerViewModel
    {
        public CustomerViewModel()
        {
            ColumnLists = new List<ItemList>() {
                new ItemList { Text = "Phone", Value = "Phone" },
                new ItemList { Text = "Mobile", Value = "Mobile" },
                new ItemList { Text = "First Name", Value = "FirstName" },
                new ItemList { Text = "Last Name", Value = "LastName" },
                new ItemList { Text = "Date Of Birth", Value = "DateOfBirth" },
            };
        }

        public CustomerColumnNameViewModel Column { get; set; }

        public List<string> AvailableColumns { get; set; } = new List<string>();

        public List<ItemList> ColumnLists { get; set; }
    }
}
