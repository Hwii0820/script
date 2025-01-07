using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Data/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("Item General Information")]
    public string itemName;         // ������ �̸�
    public Sprite itemSprite;       // ������ ��������Ʈ
    [TextArea]
    public string itemDescription;  // ������ ����

    [Header("Weapon Settings")]
    public bool isWeapon;           // ���� ���� üũ
    public WeaponStats weaponStats; // ���� Ư�� (������ ��쿡�� Ȱ��ȭ)

    [System.Serializable]
    public class WeaponStats
    {
        public int damage;           // ���ݷ�
        [Range(0, 100)] 
        public float criticalChance; // ġ��Ÿ Ȯ�� (%)
        public float attackSpeed;    // ���� �ӵ�
    }
}
