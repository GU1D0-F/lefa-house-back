namespace Core.Payments
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
    }

}
