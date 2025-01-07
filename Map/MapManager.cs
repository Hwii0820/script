using UnityEngine;

public class MapManager : MonoBehaviour
{
    public Transform roomParent;
    public RandomGenerator randomGenerator;

    private void Start()
    {
        randomGenerator.GenerateRooms(roomParent);
    }
}
