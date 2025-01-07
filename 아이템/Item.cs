using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Item : MonoBehaviour
{
    public ItemData itemData; // ������ ������ ����

    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D physicsCollider;
    private Rigidbody2D rb2D;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        physicsCollider = GetComponent<PolygonCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();

        // Rigidbody2D ����
        if (rb2D != null)
        {
            rb2D.bodyType = RigidbodyType2D.Dynamic; // ���� ����
            rb2D.gravityScale = 1; // �߷� Ȱ��ȭ
            rb2D.constraints = RigidbodyConstraints2D.FreezeRotation; // ȸ�� ����
        }

        // ������ ������ ����
        if (itemData != null)
        {
            ApplyItemData(itemData);
        }
    }

    private void ApplyItemData(ItemData data)
    {
        if (spriteRenderer != null && data.itemSprite != null)
        {
            spriteRenderer.sprite = data.itemSprite;
        }

        if (physicsCollider != null && data.itemSprite != null)
        {
            // Custom Physics Shape ����
            List<Vector2> physicsShape = new List<Vector2>();
            data.itemSprite.GetPhysicsShape(0, physicsShape);
            physicsCollider.SetPath(0, physicsShape);
        }
    }
}
