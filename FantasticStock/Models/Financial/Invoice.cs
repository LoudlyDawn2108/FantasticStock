using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasticStock.Models.Financial
{
        public class Invoice
        {
            public int InvoiceID { get; set; }
            public string InvoiceNumber { get; set; }
            public DateTime InvoiceDate { get; set; }
            public DateTime DueDate { get; set; }
            public int CustomerID { get; set; }
            public string CustomerName { get; set; }
            public decimal Amount { get; set; }
            public decimal PaidAmount { get; set; }
            public string Status { get; set; } // Open, Partial, Paid, Canceled
            public string Description { get; set; }
            public string Notes { get; set; }
            public int CreatedBy { get; set; }
            public string CreatedByName { get; set; }
            public DateTime CreatedDate { get; set; }

            // Calculated properties
            public decimal Balance => Amount - PaidAmount;
            public bool IsOverdue => Status != "Paid" && Status != "Canceled" && DueDate < DateTime.Today;
            public int DaysOverdue => IsOverdue ? (DateTime.Today - DueDate).Days : 0;
        }
}
