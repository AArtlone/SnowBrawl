using System.Collections;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private PlayerID playerID;
    [SerializeField] private Animator animator;
    [SerializeField] private GroundCheck groundCheck;

    private static readonly int ANIMATOR_IDLE = Animator.StringToHash("Idle");
    private static readonly int ANIMATOR_PICKUP = Animator.StringToHash("PickUp");
    private static readonly int ANIMATOR_THROW = Animator.StringToHash("Throw");
    private static readonly int ANIMATOR_WALK = Animator.StringToHash("Walk");
    private static readonly int ANIMATOR_JUMP = Animator.StringToHash("Jump");

    private int currentAnimationState;

    private bool isPickingUp;
    private bool isThrowing;

    private void Update()
    {
        if (GameManager.Instance.GameIsPaused)
            return;

        if (isPickingUp || isThrowing)
            return;

        if (!groundCheck.IsGrounded)
        {
            ChangeAnimationState(ANIMATOR_JUMP);
            return;
        }

        UpdateMovementAnimation();
    }

    private void UpdateMovementAnimation()
    {
        string id = playerID.ToString();

        float horizontalInput = Input.GetAxisRaw(id + " Keyboard");

        if (horizontalInput == 0)
            horizontalInput = Input.GetAxisRaw(id);

        if (horizontalInput == 0)
            ChangeAnimationState(ANIMATOR_IDLE);
        else
            ChangeAnimationState(ANIMATOR_WALK);
    }

    public void ThrowAnimation()
    {
        StartCoroutine(ThrowAnimationCo());
    }

    private IEnumerator ThrowAnimationCo()
    {
        isThrowing = true;

        ChangeAnimationState(ANIMATOR_THROW);

        float delay = animator.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(delay + .1f);

        isThrowing = true;

        ChangeAnimationState(ANIMATOR_IDLE);
    }

    public void StartPickUpAnimation()
    {
        isPickingUp = true;

        ChangeAnimationState(ANIMATOR_PICKUP);
    }

    public void StopPickUpAnimation()
    {
        if (!isPickingUp)
            return;

        isPickingUp = false;

        ChangeAnimationState(ANIMATOR_IDLE);
    }

    private void ChangeAnimationState(int state)
    {
        if (currentAnimationState == state)
            return;

        animator.Play(state);

        currentAnimationState = state;
    }
}
