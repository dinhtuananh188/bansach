using System;
using System.Collections.Generic;

namespace Sach.Model.Models
{
    public partial class OrderDetail
    {
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
    public class OrderDetailKey
    {
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
    }
}
