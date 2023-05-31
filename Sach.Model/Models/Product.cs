using System;
using System.Collections.Generic;

namespace Sach.Model.Models
{
    public partial class Product
    {
        public Product()
        {
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public int? PublishedYear { get; set; }
        public string? Image { get; set; }
        public string? MoreImage { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }
        public int? ViewCount { get; set; }
        public string? Condition { get; set; }
        public string? PublicationStatus { get; set; }
        public int? RestorationId { get; set; }

        public virtual ProductCategory? Category { get; set; }
        public virtual BookRestoration? Restoration { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
