using UnityEngine;
using UnityEngine.UI;

public class WeaponSlotUI : MonoBehaviour
{
    public Image weaponIcon;       // 무기 아이콘 이미지
    public Text weaponNameText;    // 무기 이름 텍스트
    public WeaponSlot weaponSlot;  // 연결된 WeaponSlot 데이터

    public void UpdateSlotUI()
    {
        if (weaponSlot == null || weaponSlot.IsEmpty)
        {
            // Empty 상태 처리
            weaponIcon.sprite = null;
            weaponIcon.color = new Color(1, 1, 1, 0); // 완전 투명
            weaponNameText.text = "Empty";
        }
        else
        {
            // 무기 할당 상태 처리
            weaponIcon.sprite = weaponSlot.weaponItem.itemSprite;
            weaponIcon.color = new Color(1, 1, 1, 1f);
            weaponNameText.text = weaponSlot.weaponItem.itemName;
        }
    }
}
