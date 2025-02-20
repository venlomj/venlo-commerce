using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Invoice
    {
        public string InvoiceNumber { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public List<InvoiceLineItem> LineItems { get; set; } = new();
        public decimal TotalAmount => LineItems.Sum(item => item.TotalPrice);
    }
}
