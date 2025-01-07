using UnityEngine;
using Cinemachine;

public class RoomCameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera; // 씬에 하나만 존재하는 Virtual Camera
    private CinemachineConfiner2D confiner; // Confiner 2D 컴포넌트

    private void Start()
    {
        if (virtualCamera == null)
        {
            Debug.LogError("Virtual Camera is not assigned!");
            return;
        }

        // Virtual Camera에서 Confiner2D 컴포넌트 찾기
        confiner = virtualCamera.GetComponent<CinemachineConfiner2D>();
        if (confiner == null)
        {
            Debug.LogError("Cinemachine Confiner2D is not attached to the Virtual Camera!");
            return;
        }

        // PlayerDetector 이벤트 구독
        PlayerDetector.OnPlayerEnter += SetCurrentRoom;
    }

    private void OnDestroy()
    {
        // PlayerDetector 이벤트 구독 해제
        PlayerDetector.OnPlayerEnter -= SetCurrentRoom;
    }

    public void SetCurrentRoom(Transform roomTransform)
    {
        if (roomTransform == null)
        {
            Debug.LogWarning("Room transform is null!");
            return;
        }

        // "CameraShape" 이름의 자식 오브젝트에서 PolygonCollider2D 찾기
        PolygonCollider2D cameraShapeCollider = roomTransform.Find("CameraShape")?.GetComponent<PolygonCollider2D>();
        if (cameraShapeCollider == null)
        {
            Debug.LogWarning($"No valid 'CameraShape' with PolygonCollider2D found in {roomTransform.name}.");
            return;
        }

        // Confiner2D의 Bounding Shape 2D 필드에 PolygonCollider2D 할당
        confiner.m_BoundingShape2D = cameraShapeCollider;
        Debug.Log($"Updated confiner to collider: {cameraShapeCollider.name} in room {roomTransform.name}");
    }
}
