using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private WeaponManager weaponManager;
    private WeaponInteractionHandler interactionHandler;
    private WeaponVisualHandler visualHandler;

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
        interactionHandler = GetComponent<WeaponInteractionHandler>();
        visualHandler = GetComponent<WeaponVisualHandler>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            interactionHandler.TryPickupWeapon();
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            weaponManager.SwitchWeapon();
            visualHandler.UpdateWeaponVisual(weaponManager.primaryWeapon);
        }
    }
}
