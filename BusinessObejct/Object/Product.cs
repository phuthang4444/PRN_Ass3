﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObejct.Object
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string Weight { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitslnStock { get; set; }
    }
}
