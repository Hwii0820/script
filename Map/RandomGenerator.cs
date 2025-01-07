using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    [Header("Folder Configuration")]
    public string roomPrefabFolder = "Rooms"; // Resources/Rooms 폴더

    [Header("Map Configuration")]
    public int numberOfRooms = 10; // 생성할 방 수
    public float roomSpacing = 10f; // 방 간의 간격

    private Dictionary<string, List<GameObject>> roomPrefabs;

    private void Awake()
    {
        LoadRoomPrefabs();
    }

    private void LoadRoomPrefabs()
    {
        roomPrefabs = new Dictionary<string, List<GameObject>>
        {
            { "Combat", new List<GameObject>() },
            { "Boss", new List<GameObject>() }
        };

        GameObject[] loadedPrefabs = Resources.LoadAll<GameObject>(roomPrefabFolder);

        foreach (var prefab in loadedPrefabs)
        {
            if (prefab.name.Contains("Combat"))
                roomPrefabs["Combat"].Add(prefab);
            else if (prefab.name.Contains("Boss"))
                roomPrefabs["Boss"].Add(prefab);
        }

        Debug.Log($"Loaded {loadedPrefabs.Length} room prefabs from folder: {roomPrefabFolder}");
    }

    public void GenerateRooms(Transform roomParent)
    {
        Vector2 currentPosition = Vector2.zero;

        for (int i = 0; i < numberOfRooms; i++)
        {
            string roomType = (i == numberOfRooms - 1) ? "Boss" : "Combat"; // 마지막 방은 Boss
            GameObject roomPrefab = GetRandomRoomPrefab(roomType);

            if (roomPrefab != null)
            {
                GameObject roomObject = Instantiate(roomPrefab, currentPosition, Quaternion.identity, roomParent);
                roomObject.name = $"{roomType}_Room_{i + 1}";
                currentPosition.x += roomSpacing; // 다음 방의 위치로 이동
            }
        }
    }

    private GameObject GetRandomRoomPrefab(string roomType)
    {
        if (roomPrefabs.ContainsKey(roomType) && roomPrefabs[roomType].Count > 0)
        {
            return roomPrefabs[roomType][Random.Range(0, roomPrefabs[roomType].Count)];
        }

        Debug.LogError($"No prefabs found for room type: {roomType}");
        return null;
    }
}
