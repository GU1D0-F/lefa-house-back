namespace Core.Transactions
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
    }
}
