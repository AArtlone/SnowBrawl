using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class TutorialJumpingCharacter : MonoBehaviour
{
    [SerializeField] private float jumpValue;
    [SerializeField] private float jumpFrequency;
    [SerializeField] private float initialDelay;
    [SerializeField] private GroundCheck groundCheck;

    private static readonly int ANIMATOR_GROUNDED = Animator.StringToHash("grounded");

    private Rigidbody2D rb;
    private Animator animator;

    private float currentTime;

    private bool passedInitialDelay;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        
        if (!passedInitialDelay)
        {
            if (currentTime < initialDelay)
                return;

            passedInitialDelay = true;
            currentTime = 0;
        }

        print("waiting to jump");

        if (currentTime >= jumpFrequency)
        {
            Jump();

            currentTime = 0;
        }

        animator.SetBool(ANIMATOR_GROUNDED, groundCheck.IsGrounded);
    }

    private void Jump()
    {
        rb.velocity = Vector2.up * jumpValue;
    }
}
