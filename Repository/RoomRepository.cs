using System.Collections.Generic;
using Business;
using DataAccess;

namespace Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly RoomDAO dao = new();

        public List<Room> GetAll() => dao.GetAll();
        public List<Room> GetAvailable() => dao.GetAvailable();
        public void Add(Room room) => dao.Add(room);
        public void Update(Room room) => dao.Update(room);
        public void SetAvailable(int id, bool available) => dao.SetAvailable(id, available);
    }
}
