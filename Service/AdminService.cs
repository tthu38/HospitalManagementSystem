using Business;
using Repository;

namespace Service
{
    public class AdminService
    {
        private readonly IAdminRepository repo;

        public AdminService()
        {
            repo = new AdminRepository();
        }

        public Admin? Login(string username, string password)
        {
            return repo.Login(username, password);
        }

        public void Add(Admin admin)
        {
            repo.Add(admin);
        }
    }
}
