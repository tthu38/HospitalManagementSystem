using Business;
using DataAccess;

namespace Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AdminDAO dao = AdminDAO.Instance;
        public Admin? Login(string username, string password) => dao.Login(username, password);
        public void Add(Admin admin) => dao.Add(admin);
    }
}
