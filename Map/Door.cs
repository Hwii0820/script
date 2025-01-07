using UnityEngine;

public class Door : MonoBehaviour
{
    public string doorId; // 고유 ID
    public string connectedDoorId; // 연결된 문 ID
    private DoorManager doorManager; // DoorManager 참조
    private bool isPlayerNearby = false; // 플레이어가 문에 가까이 있는지 여부

    private void Start()
    {
        // DoorManager에 자신을 등록
        doorManager = FindObjectOfType<DoorManager>();
        if (doorManager != null)
        {
            doorManager.RegisterDoor(this);
        }
    }

    private void Update()
    {
        // 플레이어가 근처에 있을 때 E 키를 눌렀을 경우
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            doorManager?.HandleDoorInteraction(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true; // 플레이어가 근처에 있음
            Debug.Log($"Player entered door area: {doorId}");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false; // 플레이어가 근처에서 벗어남
            Debug.Log($"Player exited door area: {doorId}");
        }
    }
}
