using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private List<Door> allDoors = new List<Door>();
    private PlayerMover playerMover;

    private void Start()
    {
        playerMover = FindObjectOfType<PlayerMover>(); // PlayerMover ã��
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

            // ���� �� ���� LeftDoor�� �������� ����
            if (i == 0 && currentDoor.name.Contains("LeftDoor"))
            {
                Debug.LogWarning($"{currentDoor.doorId} ({currentDoor.name}) is not connected (Left end).");
                continue;
            }

            // ������ �� ���� RightDoor�� �������� ����
            if (i == allDoors.Count - 1 && currentDoor.name.Contains("RightDoor"))
            {
                Debug.LogWarning($"{currentDoor.doorId} ({currentDoor.name}) is not connected (Right end).");
                continue;
            }

            // LeftDoor ���� (���� ���� RightDoor�� ����)
            if (currentDoor.name.Contains("LeftDoor") && i + 1 < allDoors.Count)
            {
                Door nextDoor = allDoors[i + 1];
                if (nextDoor.name.Contains("RightDoor"))
                {
                    currentDoor.connectedDoorId = nextDoor.doorId;
                    nextDoor.connectedDoorId = currentDoor.doorId;
                    Debug.Log($"Connected {currentDoor.doorId} ({currentDoor.name}) �� {nextDoor.doorId} ({nextDoor.name})");
                }
            }

            // RightDoor ���� (���� ���� LeftDoor�� ����)
            if (currentDoor.name.Contains("RightDoor") && i - 1 >= 0)
            {
                Door previousDoor = allDoors[i - 1];
                if (previousDoor.name.Contains("LeftDoor"))
                {
                    currentDoor.connectedDoorId = previousDoor.doorId;
                    previousDoor.connectedDoorId = currentDoor.doorId;
                    Debug.Log($"Connected {currentDoor.doorId} ({currentDoor.name}) �� {previousDoor.doorId} ({previousDoor.name})");
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
