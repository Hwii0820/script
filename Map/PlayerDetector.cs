using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public bool IsPlayerInside { get; private set; } = false; // 플레이어가 방 안에 있는지 여부

    public delegate void PlayerDetected(Transform roomTransform); // 이벤트 델리게이트
    public static event PlayerDetected OnPlayerEnter; // 플레이어가 방에 진입했을 때 발생

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerInside = true;
            Debug.Log($"Player entered: {gameObject.name}");
            OnPlayerEnter?.Invoke(transform.parent); // 부모 방 Transform 전달
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerInside = false;
            Debug.Log($"Player exited: {gameObject.name}");
        }
    }
}
