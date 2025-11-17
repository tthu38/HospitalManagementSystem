using System.Collections.Generic;
using Business;
using Repository;

namespace Service
{
    public class NotificationService
    {
        private readonly INotificationRepository repo;

        public NotificationService()
        {
            repo = new NotificationRepository();
        }

        public void Create(Notification notification)
        {
            repo.Add(notification);
        }
        public List<Notification> GetForUser(string role, int id, bool onlyUnread = false)
        {
            return repo.GetForUser(role, id, onlyUnread);
        }

        public void MarkAsRead(int notificationId)
        {
            repo.MarkAsRead(notificationId);
        }
    }
}
