using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories.Repositories
{
    public class FarmOwnerRepository : GenericRepository<FarmOwner>
    {
        public FarmOwnerRepository() {
         this._context ??= new Context();
        }
        public FarmOwnerRepository(Context context) 
        {
          this._context = context;
        }

        public async Task<FarmOwner> SignIn(string email, string password)
        {
            var user = (await this.FindByConditionAsync(x => x.Email == email && x.Password == password)).FirstOrDefault();
            return user;
        }
    }
}
