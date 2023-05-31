using System;
using System.Collections.Generic;

namespace Sach.Model.Models
{
    public partial class Review
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? CustomerId { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Product? Product { get; set; }
    }
}
