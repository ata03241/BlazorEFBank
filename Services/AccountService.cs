namespace BlazorEFIdentity.Services;
using Microsoft.EntityFrameworkCore;
using BlazorEFIdentity.Model;
using BlazorEFIdentity.Data;


    public class AccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Account> GetAccountById(int accountId)
        {
            return await _context.Accounts
                .Where(a => a.Id == accountId)
                .FirstOrDefaultAsync();
    }

        public async Task<List<Transaction>> GetTransactionAsync(int accountId)
        {
            return await _context.Transactions
                .Include(t => t.FromAccount)
                .Where(t => t.FromAccountId == accountId || t.ToAccountId == accountId)
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }

}
