using Xunit;
using Moq;
using Repository;
using Service;
using Business;

using Xunit;
using Moq;
using Repository;
using Service;
using Business;
using System.Collections.Generic;

namespace HospitalApp.UnitTests
{
    public class RoomServiceTests
    {
        [Fact]
        public void GetAll_ReturnsAllRoomsFromRepository()
        {

            var mockRepo = new Mock<IRoomRepository>();
            var expectedRooms = new List<Room> { new Room(), new Room() };
            mockRepo.Setup(repo => repo.GetAll()).Returns(expectedRooms);

            var roomService = new RoomService { repo = mockRepo.Object };

            var actualRooms = roomService.GetAll();

            mockRepo.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public void GetAvailable_ReturnsAvailableRoomsFromRepository()
        {

            var mockRepo = new Mock<IRoomRepository>();
            var expectedRooms = new List<Room> { new Room(), new Room() };
            mockRepo.Setup(repo => repo.GetAvailable()).Returns(expectedRooms);

            var roomService = new RoomService { repo = mockRepo.Object };

            var actualRooms = roomService.GetAvailable();

            mockRepo.Verify(repo => repo.GetAvailable(), Times.Once);
        }

        [Fact]
        public void Create_AddsRoomToRepository()
        {

            var mockRepo = new Mock<IRoomRepository>();
            var roomToCreate = new Room();
            var roomService = new RoomService { repo = mockRepo.Object };

            roomService.Create(roomToCreate);

            mockRepo.Verify(repo => repo.Add(roomToCreate), Times.Once);
        }

        [Fact]
        public void Update_UpdatesRoomInRepository()
        {

            var mockRepo = new Mock<IRoomRepository>();
            var roomToUpdate = new Room();
            var roomService = new RoomService { repo = mockRepo.Object };

            roomService.Update(roomToUpdate);

            mockRepo.Verify(repo => repo.Update(roomToUpdate), Times.Once);
        }

        [Fact]
        public void SetAvailable_SetsAvailabilityInRepository()
        {

            var mockRepo = new Mock<IRoomRepository>();
            int roomId = 1;
            bool available = true;
            var roomService = new RoomService { repo = mockRepo.Object };

            roomService.SetAvailable(roomId, available);

            mockRepo.Verify(repo => repo.SetAvailable(roomId, available), Times.Once);
        }
    }

    public class RoomService
    {
        public IRoomRepository repo;

        public RoomService()
        {
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