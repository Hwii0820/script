using UnityEngine;

public class WeaponHolderController : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform parent;
    public Transform targetPosition;
    public float followDelay = 0.2f;
    public float followSpeed = 5f;

    [Header("Pendulum Settings")]
    public float swingAmplitude = 15f;
    public float returnSpeed = 2f;

    private GameObject currentWeapon;
    private Vector3 delayedPosition;
    private float currentAngle = 0f;
    private float previousParentY;

    void Start()
    {
        if (parent == null)
        {
            parent = transform.parent;
        }

        delayedPosition = targetPosition.position;
        previousParentY = parent.position.y;
    }

    void Update()
    {
        if (parent == null || targetPosition == null) return;

        FollowTargetWithDelay();
        ApplyPendulumEffect();
    }

    public void SetCurrentWeapon(GameObject weapon)
    {
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false);
        }

        currentWeapon = weapon;

        if (currentWeapon != null)
        {
            currentWeapon.SetActive(true);
            currentWeapon.transform.SetParent(targetPosition);
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.identity;
        }
    }

    private void FollowTargetWithDelay()
    {
        delayedPosition = Vector3.Lerp(delayedPosition, targetPosition.position, followSpeed * Time.deltaTime);
        transform.position = delayedPosition;
    }

    private void ApplyPendulumEffect()
    {
        float parentYChange = parent.position.y - previousParentY;

        currentAngle -= parentYChange * swingAmplitude;
        currentAngle = Mathf.Lerp(currentAngle, 0, returnSpeed * Time.deltaTime);

        transform.localRotation = Quaternion.Euler(0, 0, currentAngle);
        previousParentY = parent.position.y;
    }
}
