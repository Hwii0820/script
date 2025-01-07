using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public void TeleportPlayer(Transform player, Vector3 targetPosition)
    {
        if (player != null)
        {
            player.position = targetPosition;
            Debug.Log($"Teleported player to position: {targetPosition}");
        }
        else
        {
            Debug.LogError("Player reference is null!");
        }
    }
}
