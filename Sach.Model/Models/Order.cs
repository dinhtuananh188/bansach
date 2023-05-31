﻿using System;
using System.Collections.Generic;

namespace Sach.Model.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerMobile { get; set; }
        public string? CustomerMessage { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? PaymentMethod { get; set; }
        public bool? PaymentStatus { get; set; }
        public bool? Status { get; set; }
    }
}
