using System.Linq;
using Business;

namespace DataAccess
{
    public class AdminDAO
    {
        private static AdminDAO? instance;
        private static readonly object lockObj = new();
        private AdminDAO() { }

        public static AdminDAO Instance
        {
            get
            {
                lock (lockObj)
                {
                    return instance ??= new AdminDAO();
                }
            }
        }

        public Admin? Login(string username, string password)
        {
            using var context = new HospitalManagementContext();
            return context.Admins.FirstOrDefault(a => a.Username == username && a.Password == password);
        }

        public void Add(Admin admin)
        {
            using var context = new HospitalManagementContext();
            context.Admins.Add(admin);
            context.SaveChanges();
        }
    }
}
