using System;
using System.Collections.Generic;

namespace Sach.Model.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? Address { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
