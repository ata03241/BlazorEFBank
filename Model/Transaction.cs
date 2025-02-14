namespace BlazorEFIdentity.Model
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal outgoingbalance { get; set; }
        public bool IsReserved { get; set; }
        public string? Messages { get; set; }
        public int FromAccountId { get; set; }
        public Account FromAccount { get; set; }
        public string ToAccountName { get; set; }
        public int ToAccountId { get; set; }

        

    }
}
