using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObejct.Object
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int MemberId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal? Freight { get; set; }
    }
}
