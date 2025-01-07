using UnityEngine;

public class WeaponSlot
{
    public ItemData weaponItem; // ���� �����͸� ������ ������ ������

    public bool IsEmpty => weaponItem == null;

    public void Equip(ItemData newItem)
    {
        if (newItem != null && newItem.isWeapon)
        {
            weaponItem = newItem; // ���� üũ�� Ȱ��ȭ�� �����۸� ���� ����
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
