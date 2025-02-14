using BlazorEFIdentity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorEFIdentity.Model
{
    
    public class Account
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string AccountType { get; set; }
        public List<Transaction>? Transactions { get; set; } = new List<Transaction>();
        public bool IsActive { get; set; }
        public string? CardNumber { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
