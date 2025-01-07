using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private List<Door> allDoors = new List<Door>();
    private PlayerMover playerMover;

    private void Start()
    {
        playerMover = FindObjectOfType<PlayerMover>(); // PlayerMover 찾기
        if (playerMover == null)
        {
            Debug.LogError("PlayerMover component not found in the scene!");
        }

        InitializeDoors();
        ConnectDoorsInSequence();
    }

    public void RegisterDoor(Door door)
    {
        if (!allDoors.Contains(door))
        {
            allDoors.Add(door);
            door.doorId = $"Door_{allDoors.Count}";
        }
    }

    private void InitializeDoors()
    {
        Door[] doors = FindObjectsOfType<Door>();
        foreach (Door door in doors)
        {
            RegisterDoor(door);
        }
    }

    private void ConnectDoorsInSequence()
    {
        for (int i = 0; i < allDoors.Count; i++)
        {
            Door currentDoor = allDoors[i];

            // 왼쪽 끝 방의 LeftDoor는 연결하지 않음
            if (i == 0 && currentDoor.name.Contains("LeftDoor"))
            {
                Debug.LogWarning($"{currentDoor.doorId} ({currentDoor.name}) is not connected (Left end).");
                continue;
            }

            // 오른쪽 끝 방의 RightDoor는 연결하지 않음
            if (i == allDoors.Count - 1 && currentDoor.name.Contains("RightDoor"))
            {
                Debug.LogWarning($"{currentDoor.doorId} ({currentDoor.name}) is not connected (Right end).");
                continue;
            }

            // LeftDoor 연결 (다음 방의 RightDoor와 연결)
            if (currentDoor.name.Contains("LeftDoor") && i + 1 < allDoors.Count)
            {
                Door nextDoor = allDoors[i + 1];
                if (nextDoor.name.Contains("RightDoor"))
                {
                    currentDoor.connectedDoorId = nextDoor.doorId;
                    nextDoor.connectedDoorId = currentDoor.doorId;
                    Debug.Log($"Connected {currentDoor.doorId} ({currentDoor.name}) ↔ {nextDoor.doorId} ({nextDoor.name})");
                }
            }

            // RightDoor 연결 (이전 방의 LeftDoor와 연결)
            if (currentDoor.name.Contains("RightDoor") && i - 1 >= 0)
            {
                Door previousDoor = allDoors[i - 1];
                if (previousDoor.name.Contains("LeftDoor"))
                {
                    currentDoor.connectedDoorId = previousDoor.doorId;
                    previousDoor.connectedDoorId = currentDoor.doorId;
                    Debug.Log($"Connected {currentDoor.doorId} ({currentDoor.name}) ↔ {previousDoor.doorId} ({previousDoor.name})");
                }
            }
        }
    }

    public void HandleDoorInteraction(Door door)
    {
        Door connectedDoor = allDoors.Find(d => d.doorId == door.connectedDoorId);
        if (connectedDoor != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null && playerMover != null)
            {
                playerMover.TeleportPlayer(player.transform, connectedDoor.transform.position);
            }
        }
    }
}
