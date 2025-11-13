using System.Collections.Generic;
using System.Linq;
using Business;

namespace DataAccess
{
    public class RoomDAO
    {
        public List<Room> GetAll()
        {
            using var context = new HospitalManagementContext();
            return context.Rooms.OrderBy(r => r.RoomNumber).ToList();
        }

        public List<Room> GetAvailable()
        {
            using var context = new HospitalManagementContext();
            return context.Rooms.Where(r => r.IsAvailable == true).OrderBy(r => r.RoomNumber).ToList();
        }

        public void Add(Room room)
        {
            using var context = new HospitalManagementContext();
            context.Rooms.Add(room);
            context.SaveChanges();
        }

        public void Update(Room room)
        {
            using var context = new HospitalManagementContext();
            var existing = context.Rooms.FirstOrDefault(r => r.RoomId == room.RoomId);
            if (existing == null) return;

            existing.RoomNumber = room.RoomNumber;
            existing.RoomType = room.RoomType;
            existing.IsAvailable = room.IsAvailable;
            context.SaveChanges();
        }

        public void SetAvailable(int id, bool available)
        {
            using var context = new HospitalManagementContext();
            var room = context.Rooms.FirstOrDefault(r => r.RoomId == id);
            if (room == null) return;

            room.IsAvailable = available;
            context.SaveChanges();
        }
    }
}
