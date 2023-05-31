using System;
using System.Collections.Generic;

namespace Sach.Model.Models
{
    public partial class WishList
    {
        public int? Id { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? AddedDate { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Product? IdNavigation { get; set; }
    }
}
