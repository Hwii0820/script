using UnityEngine;

public class WeaponInteractionHandler : MonoBehaviour
{
    public Transform dropPosition;
    private WeaponManager weaponManager;

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
    }

    public void TryPickupWeapon()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1.5f);
        foreach (var collider in hitColliders)
        {
            Item item = collider.GetComponent<Item>();
            if (item != null && item.itemData != null && item.itemData.isWeapon)
            {
                PickupWeapon(item.itemData);
                Destroy(item.gameObject);
                break;
            }
        }
    }

    public void PickupWeapon(ItemData newWeapon)
    {
        if (weaponManager.primaryWeapon == null)
        {
            weaponManager.primaryWeapon = newWeapon;
        }
        else if (weaponManager.secondaryWeapon == null)
        {
            weaponManager.secondaryWeapon = newWeapon;
        }
        else
        {
            DropWeapon(weaponManager.primaryWeapon);
            weaponManager.primaryWeapon = newWeapon;
        }

        weaponManager.UpdateAllSlotsUI();

        // 무기 시각화 업데이트 추가
        WeaponVisualHandler visualHandler = GetComponent<WeaponVisualHandler>();
        if (visualHandler != null)
        {
            visualHandler.UpdateWeaponVisual(weaponManager.primaryWeapon);
        }
    }

    private void DropWeapon(ItemData weapon)
    {
        if (weapon == null || dropPosition == null) return;

        GameObject weaponObj = new GameObject(weapon.itemName);
        weaponObj.transform.position = dropPosition.position;

        SpriteRenderer renderer = weaponObj.AddComponent<SpriteRenderer>();
        renderer.sprite = weapon.itemSprite;

        Item item = weaponObj.AddComponent<Item>();
        item.itemData = weapon;
    }
}
