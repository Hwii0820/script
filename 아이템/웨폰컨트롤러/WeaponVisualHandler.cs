using UnityEngine;

public class WeaponVisualHandler : MonoBehaviour
{
    public WeaponHolderController weaponHolderController;
    private GameObject currentWeaponVisual;

    public void UpdateWeaponVisual(ItemData weapon)
    {
        if (weaponHolderController == null || weapon == null || !weapon.isWeapon)
        {
            if (currentWeaponVisual != null)
            {
                Destroy(currentWeaponVisual);
                currentWeaponVisual = null;
            }
            return;
        }

        if (currentWeaponVisual != null)
        {
            Destroy(currentWeaponVisual);
        }

        currentWeaponVisual = new GameObject("WeaponVisual");
        currentWeaponVisual.transform.SetParent(weaponHolderController.transform);
        currentWeaponVisual.transform.localPosition = Vector3.zero;

        SpriteRenderer renderer = currentWeaponVisual.AddComponent<SpriteRenderer>();
        renderer.sprite = weapon.itemSprite;
    }
}
