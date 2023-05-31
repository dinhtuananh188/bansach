using System;
using System.Collections.Generic;

namespace Sach.Model.Models
{
    public partial class BookRestoration
    {
        public BookRestoration()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public DateTime? RestorationDate { get; set; }
        public string? RestoredBy { get; set; }
        public string? Notes { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
