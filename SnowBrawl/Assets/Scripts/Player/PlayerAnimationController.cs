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

    private void Awake()
    {
        GameManager.onRoundOver += OnRoundOver;
    }

    private void OnDestroy()
    {
        GameManager.onRoundOver -= OnRoundOver;
    }

    private void OnRoundOver()
    {
        animator.enabled = false;
    }

    private void Update()
    {
        if (GameManager.GameIsPaused)
            return;

        if (isThrowing)
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
        float horizontalInput = SBInputManager.Instance.GetPlayerInput(playerID);

        if (horizontalInput == 0 && !isPickingUp)
            ChangeAnimationState(ANIMATOR_IDLE);
        else if (horizontalInput != 0)
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

        isThrowing = false;

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

    private GameManager GameManager { get { return GameManager.Instance; } }
}
