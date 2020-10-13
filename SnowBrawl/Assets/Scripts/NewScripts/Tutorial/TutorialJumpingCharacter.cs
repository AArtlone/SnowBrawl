using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class TutorialJumpingCharacter : TutorialCharacter
{
    [SerializeField] private float jumpValue;

    private static readonly int ANIMATOR_JUMP = Animator.StringToHash("Jump");
    private static readonly int ANIMATOR_IDLE = Animator.StringToHash("Idle");

    private Rigidbody2D rb;

    private int currentAnimationState;

    protected override void Awake()
    {
        base.Awake();
        
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
            ChangeAnimationState(ANIMATOR_IDLE);
        else if (col.GetComponent<TutorialSnowball>() != null)
            Jump();
    }

    private void OnEnable()
    {
        animator.Play(ANIMATOR_IDLE);
    }

    private void OnDisable()
    {
        transform.position = spawnPos;
    }

    private void Jump()
    {
        ChangeAnimationState(ANIMATOR_JUMP);

        rb.velocity = Vector2.up * jumpValue;
    }

    private void ChangeAnimationState(int state)
    {
        if (currentAnimationState == state)
            return;
        
        animator.Play(state);
        
        currentAnimationState = state;
    }
}
