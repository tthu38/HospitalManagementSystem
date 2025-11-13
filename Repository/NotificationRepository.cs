using System.Collections.Generic;
using Business;
using DataAccess;

namespace Repository
{
    public class NotificationRepository : INotificationRepository
    {
        // ✅ Dùng Singleton instance, không new nữa
        private readonly NotificationDAO dao = NotificationDAO.Instance;

        public void Add(Notification notification) => dao.Add(notification);

        public List<Notification> GetForUser(string role, int id, bool onlyUnread = false)
            => dao.GetForUser(role, id, onlyUnread);

        public void MarkAsRead(int notificationId) => dao.MarkAsRead(notificationId);
    }
}
