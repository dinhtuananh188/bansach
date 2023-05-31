namespace BanSachCu.Models
{
    internal class OrderViewModel
    {
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerMessage { get; set; }
        public string PaymentMethod { get; set; }
        public double TotalAmount { get; set; }
    }
}