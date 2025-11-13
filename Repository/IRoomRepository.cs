using System.Collections.Generic;
using Business;

namespace Repository
{
    public interface IRoomRepository
    {
        List<Room> GetAll();
        List<Room> GetAvailable();
        void Add(Room room);
        void Update(Room room);
        void SetAvailable(int id, bool available);
    }
}
