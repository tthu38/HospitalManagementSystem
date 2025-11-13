using System.Collections.Generic;
using System.Linq;
using Business;

namespace DataAccess
{
    public class NotificationDAO
    {
        // 🔒 1. Instance Singleton duy nhất
        private static readonly NotificationDAO instance = new NotificationDAO();
        public static NotificationDAO Instance => instance;

        // 🔒 2. Constructor private ngăn tạo mới
        private NotificationDAO() { }

        // ==========================
        // 3. Các phương thức DAO
        // ==========================

        /// <summary>
        /// Thêm thông báo mới vào hệ thống.
        /// </summary>
        public void Add(Notification notification)
        {
            using var context = new HospitalManagementContext();
            context.Notifications.Add(notification);
            context.SaveChanges();
        }

        /// <summary>
        /// Lấy danh sách thông báo của người dùng (theo Role + ID).
        /// Nếu onlyUnread = true thì chỉ lấy thông báo chưa đọc.
        /// </summary>
        public List<Notification> GetForUser(string role, int recipientId, bool onlyUnread = false)
        {
            using var context = new HospitalManagementContext();
            var query = context.Notifications
                               .Where(n => n.RecipientRole == role && n.RecipientId == recipientId);

            if (onlyUnread)
                query = query.Where(n => n.IsRead == false || n.IsRead == null);

            return query.OrderByDescending(n => n.CreatedAt).ToList();
        }

        /// <summary>
        /// Đánh dấu một thông báo là đã đọc.
        /// </summary>
        public void MarkAsRead(int notificationId)
        {
            using var context = new HospitalManagementContext();
            var noti = context.Notifications.FirstOrDefault(n => n.NotificationId == notificationId);
            if (noti == null) return;

            noti.IsRead = true;
            context.SaveChanges();
        }
    }
}
