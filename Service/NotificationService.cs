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

        /// <summary>
        /// Tạo thông báo mới (cho bác sĩ, bệnh nhân hoặc admin)
        /// </summary>
        public void Create(Notification notification)
        {
            repo.Add(notification);
        }

        /// <summary>
        /// Lấy danh sách thông báo theo vai trò (Doctor, Patient, Admin)
        /// </summary>
        public List<Notification> GetForUser(string role, int id, bool onlyUnread = false)
        {
            return repo.GetForUser(role, id, onlyUnread);
        }

        /// <summary>
        /// Đánh dấu một thông báo là đã đọc
        /// </summary>
        public void MarkAsRead(int notificationId)
        {
            repo.MarkAsRead(notificationId);
        }
    }
}
