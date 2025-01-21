using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class AccountRepository : GenericRepository<Account>
    {
        public AccountRepository()
        {
            _context ??= new Context();
        }

        public AccountRepository(Context context)
        {
            _context = context;
        }

        public IQueryable<Account> GetAccounts()
        {
            return _context.Accounts
                .Include(a => a.Farmers)
                .Include(a => a.Experts);
        }

        public async Task<Account> SignIn(string email, string password)
        {
            var user = (await this.FindByConditionAsync(x => x.Email == email && x.Password == password)).FirstOrDefault();
            return user;
        }
    }
}
