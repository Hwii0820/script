using UnityEngine;
using UnityEngine.UI;

public class WeaponSlotUI : MonoBehaviour
{
    public Image weaponIcon;       // ���� ������ �̹���
    public Text weaponNameText;    // ���� �̸� �ؽ�Ʈ
    public WeaponSlot weaponSlot;  // ����� WeaponSlot ������

    public void UpdateSlotUI()
    {
        if (weaponSlot == null || weaponSlot.IsEmpty)
        {
            // Empty ���� ó��
            weaponIcon.sprite = null;
            weaponIcon.color = new Color(1, 1, 1, 0); // ���� ����
            weaponNameText.text = "Empty";
        }
        else
        {
            // ���� �Ҵ� ���� ó��
            weaponIcon.sprite = weaponSlot.weaponItem.itemSprite;
            weaponIcon.color = new Color(1, 1, 1, 1f);
            weaponNameText.text = weaponSlot.weaponItem.itemName;
        }
    }
}
