using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObejct.Object
{
    public partial class OrderDetail
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
    }
}
