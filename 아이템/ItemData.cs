using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Data/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("Item General Information")]
    public string itemName;         // 아이템 이름
    public Sprite itemSprite;       // 아이템 스프라이트
    [TextArea]
    public string itemDescription;  // 아이템 설명

    [Header("Weapon Settings")]
    public bool isWeapon;           // 무기 여부 체크
    public WeaponStats weaponStats; // 무기 특성 (무기일 경우에만 활성화)

    [System.Serializable]
    public class WeaponStats
    {
        public int damage;           // 공격력
        [Range(0, 100)] 
        public float criticalChance; // 치명타 확률 (%)
        public float attackSpeed;    // 공격 속도
    }
}
