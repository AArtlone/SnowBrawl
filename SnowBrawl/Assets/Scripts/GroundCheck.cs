using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private Collider2D groundCheckCol;
    public bool IsGrounded { get; private set; }

    //private void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.gameObject.CompareTag("Ground"))
    //        IsGrounded = true;
    //}

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            IsGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            IsGrounded = false;
    }
}
