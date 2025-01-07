using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Item : MonoBehaviour
{
    public ItemData itemData; // 아이템 데이터 참조

    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D physicsCollider;
    private Rigidbody2D rb2D;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        physicsCollider = GetComponent<PolygonCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();

        // Rigidbody2D 설정
        if (rb2D != null)
        {
            rb2D.bodyType = RigidbodyType2D.Dynamic; // 물리 적용
            rb2D.gravityScale = 1; // 중력 활성화
            rb2D.constraints = RigidbodyConstraints2D.FreezeRotation; // 회전 고정
        }

        // 아이템 데이터 적용
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
            // Custom Physics Shape 적용
            List<Vector2> physicsShape = new List<Vector2>();
            data.itemSprite.GetPhysicsShape(0, physicsShape);
            physicsCollider.SetPath(0, physicsShape);
        }
    }
}
