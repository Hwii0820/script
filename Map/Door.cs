using UnityEngine;

public class Door : MonoBehaviour
{
    public string doorId; // ���� ID
    public string connectedDoorId; // ����� �� ID
    private DoorManager doorManager; // DoorManager ����
    private bool isPlayerNearby = false; // �÷��̾ ���� ������ �ִ��� ����

    private void Start()
    {
        // DoorManager�� �ڽ��� ���
        doorManager = FindObjectOfType<DoorManager>();
        if (doorManager != null)
        {
            doorManager.RegisterDoor(this);
        }
    }

    private void Update()
    {
        // �÷��̾ ��ó�� ���� �� E Ű�� ������ ���
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            doorManager?.HandleDoorInteraction(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true; // �÷��̾ ��ó�� ����
            Debug.Log($"Player entered door area: {doorId}");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false; // �÷��̾ ��ó���� ���
            Debug.Log($"Player exited door area: {doorId}");
        }
    }
}
