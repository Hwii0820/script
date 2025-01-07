using UnityEngine;

public class WeaponSlot
{
    public ItemData weaponItem; // 무기 데이터를 포함한 아이템 데이터

    public bool IsEmpty => weaponItem == null;

    public void Equip(ItemData newItem)
    {
        if (newItem != null && newItem.isWeapon)
        {
            weaponItem = newItem; // 무기 체크가 활성화된 아이템만 장착 가능
        }
        else
        {
            Debug.LogWarning("Equip failed: The item is not a weapon.");
        }
    }

    public ItemData Unequip()
    {
        ItemData unequippedItem = weaponItem;
        weaponItem = null;
        return unequippedItem;
    }
}
