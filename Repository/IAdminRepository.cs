using Business;

namespace Repository
{
    public interface IAdminRepository
    {
        Admin? Login(string username, string password);
        void Add(Admin admin);
    }
}
