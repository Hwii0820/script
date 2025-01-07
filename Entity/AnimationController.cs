using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetWalking(bool isWalking)
    {
        animator.SetBool("IsWalking", isWalking);
    }

    public void SetRunning(bool isRunning)
    {
        animator.SetBool("IsRunning", isRunning);
    }

    public void SetBackstepping(bool isBackstepping)
    {
        animator.SetBool("IsBackstepping", isBackstepping);
    }

    public void SetIsJumping(bool isJumping)
    {
        animator.SetBool("IsJumping", isJumping);
    }

    public void TriggerDash()
    {
        animator.SetTrigger("Dash");
        animator.ResetTrigger("Dash");
    }
}
