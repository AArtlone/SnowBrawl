using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement
{
    private MovementSettings mvSettings;
    private GroundCheck groundCheck;
    private Rigidbody2D rbToMove;
    private KeyCode jumpKey = KeyCode.W;

    public PlayerMovement(MovementSettings mvSettings, Rigidbody2D rb, GroundCheck groundCheck)
    {
        this.mvSettings = mvSettings;

        rbToMove = rb;

        this.groundCheck = groundCheck;
    }

    public void UpdateVelocity()
    {
        UpdateHorizontalVelocity();
        UpdateVerticalVelocity();
    }

    private void UpdateHorizontalVelocity()
    {
        float xValue = Input.GetAxis("P1Horizontal");

        if (xValue == 0)
        {
            rbToMove.velocity = new Vector2(0, rbToMove.velocity.y);
            return;
        }

        rbToMove.AddForce(Vector2.right * mvSettings.speed * xValue);

        float xVel = rbToMove.velocity.x;

        xVel = Mathf.Clamp(xVel, mvSettings.minSpeed, mvSettings.maxSpeed);

        rbToMove.velocity = new Vector2(xVel, rbToMove.velocity.y);
    }

    private void UpdateVerticalVelocity()
    {
        if (!groundCheck.IsGrounded)
            return;

        if (Input.GetKey(jumpKey))
            rbToMove.velocity = Vector2.up * mvSettings.springAbility;
    }
}
