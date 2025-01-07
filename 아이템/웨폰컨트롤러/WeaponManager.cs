using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponSlot primarySlot = new WeaponSlot();
    public WeaponSlot secondarySlot = new WeaponSlot();
    public WeaponSlotUI primarySlotUI;
    public WeaponSlotUI secondarySlotUI;

    public ItemData primaryWeapon;
    public ItemData secondaryWeapon;

    private void Start()
    {
        UpdateAllSlotsUI();
    }

    public void SwitchWeapon()
    {
        var temp = primaryWeapon;
        primaryWeapon = secondaryWeapon;
        secondaryWeapon = temp;

        primarySlot.Equip(primaryWeapon);
        secondarySlot.Equip(secondaryWeapon);

        UpdateAllSlotsUI();
    }

    public void UpdateAllSlotsUI()
    {
        UpdateSlotUI(primarySlot, primaryWeapon, primarySlotUI);
        UpdateSlotUI(secondarySlot, secondaryWeapon, secondarySlotUI);
    }

    private void UpdateSlotUI(WeaponSlot slot, ItemData weapon, WeaponSlotUI ui)
    {
        if (weapon == null)
        {
            ui.weaponIcon.sprite = null;
            ui.weaponIcon.color = new Color(1, 1, 1, 0);
            ui.weaponNameText.text = "Empty";
        }
        else
        {
            slot.Equip(weapon);
            ui.weaponSlot = slot;
            ui.UpdateSlotUI();
        }
    }
}
