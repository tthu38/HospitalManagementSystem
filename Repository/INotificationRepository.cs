using System.Collections.Generic;
using Business;

namespace Repository
{
    public interface INotificationRepository
    {
        void Add(Notification notification);
        List<Notification> GetForUser(string role, int id, bool onlyUnread = false);
        void MarkAsRead(int notificationId);
    }
}
