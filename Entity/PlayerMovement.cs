using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeedMultiplier = 1.5f;
    public float jumpForce = 10f;
    public float dashSpeed = 15f;
    private bool canDash = true;
    private bool canJump = true;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private AnimationController animationController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animationController = GetComponent<AnimationController>();
    }

    void Update()
    {
        Move();
        FlipSpriteTowardsMouse();

        if (Input.GetButtonDown("Jump") && canJump)
        {
            Jump();
        }

        if (Input.GetMouseButtonDown(0) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float speed = moveSpeed;

        // 이동 중인지 확인
        bool isMoving = horizontal != 0;

        // 달리기 상태는 이동 중이고 LeftShift 키를 누른 경우
        bool isRunning = isMoving && Input.GetKey(KeyCode.LeftShift);
        bool isBackstep = isRunning && IsBackstepping(horizontal);
        bool isWalking = isMoving && !isRunning && !isBackstep;

        if (isRunning)
        {
            speed *= runSpeedMultiplier;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        // 애니메이션 상태 업데이트
        animationController.SetWalking(isWalking);
        animationController.SetRunning(isRunning && !isBackstep);
        animationController.SetBackstepping(isBackstep);
    }

    bool IsBackstepping(float horizontal)
    {
        float mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        return (mouseX > transform.position.x && horizontal < 0) || 
               (mouseX < transform.position.x && horizontal > 0);
    }

    void FlipSpriteTowardsMouse()
    {
        float mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        bool shouldFlip = mouseX < transform.position.x;

        if (shouldFlip != (transform.localScale.x < 0))
        {
            FlipAllChildObjects(shouldFlip);
        }
    }

    public void FlipAllChildObjects(bool flip)
    {
        Vector3 newScale = transform.localScale;
        newScale.x = flip ? -Mathf.Abs(newScale.x) : Mathf.Abs(newScale.x);
        transform.localScale = newScale;

        foreach (Transform child in transform)
        {
            SpriteRenderer childRenderer = child.GetComponent<SpriteRenderer>();
            if (childRenderer != null)
            {
                childRenderer.flipX = false;
            }
        }
    }

    void Jump()
    {
        canJump = false; // 점프 금지
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        animationController.SetIsJumping(true);
    }

    IEnumerator Dash()
    {
        canDash = false;
        animationController.TriggerDash();
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed, rb.velocity.y);
        yield return new WaitForSeconds(0.2f);
        rb.velocity = new Vector2(0, rb.velocity.y);
        yield return new WaitForSeconds(1f);
        canDash = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true; // 착지 시 점프 가능
            animationController.SetIsJumping(false);
        }
    }
}
