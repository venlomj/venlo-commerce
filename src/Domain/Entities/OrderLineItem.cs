using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderLineItem : BaseEntity
    {
        public Guid Id { get; set; }
        public string SkuCode { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
