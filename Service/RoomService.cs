using System.Collections.Generic;
using Business;
using Repository;

namespace Service
{
    public class RoomService
    {
        private readonly IRoomRepository repo;

        public RoomService()
        {
            repo = new RoomRepository();
        }

        public List<Room> GetAll() => repo.GetAll();

        public List<Room> GetAvailable() => repo.GetAvailable();

        public void Create(Room room)
        {
            repo.Add(room);
        }

        public void Update(Room room)
        {
            repo.Update(room);
        }

        public void SetAvailable(int id, bool available)
        {
            repo.SetAvailable(id, available);
        }
    }
}
