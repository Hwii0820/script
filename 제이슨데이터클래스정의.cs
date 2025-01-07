[System.Serializable]
public class Room
{
    public string roomId;
    public string type;
    public string[] connectedRooms;
}

[System.Serializable]
public class Stage
{
    public int stageId;
    public Room[] rooms;
}

[System.Serializable]
public class StageData
{
    public Stage[] stages;
}
