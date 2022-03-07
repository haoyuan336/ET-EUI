using System.Collections.Generic;

namespace ET.Room
{
    public static class RoomComponentSystem
    {
        public static void CreateRoom(this RoomComponent self, List<Unit> units)
        {
            // Log.Debug("creat room units");
            // self.AddChild<Room>()
            long id = IdGenerater.Instance.GenerateId();
            Room room = self.AddChildWithId<Room>(id);
            room.AddUnits(units);
        }
    }
}