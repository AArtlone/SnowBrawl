using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private Collider2D groundCheckCol;
    public bool IsGrounded { get; private set; }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            IsGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            IsGrounded = false;
    }

    public void DisableCollision(Collider2D col)
    {
        Physics2D.IgnoreCollision(groundCheckCol, col);
    }
}
