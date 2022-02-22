using GatewayAPI.Models;

namespace GatewayAPI.Services
{
    public class UserRepo 
    {
        private readonly UserContext _context;

        public UserRepo(UserContext context)
        {
            _context = context;
         
        }

        public async Task<User> GetUserDetail(string username)
        {
            var myUser = _context.Users.SingleOrDefault(u => u.Username == username);
            if(myUser == null)
                return null;
            return myUser;
        }
    }
}
