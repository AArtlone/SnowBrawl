﻿using System.Collections;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private PlayerID playerId;
    [SerializeField] private Animator animator;
    [SerializeField] private GroundCheck groundCheck;

    private static readonly int ANIMATOR_SPEED = Animator.StringToHash("speed");
    private static readonly int ANIMATOR_GROUNDED = Animator.StringToHash("grounded");
    private static readonly int ANIMATOR_SHOOTING = Animator.StringToHash("shooting");
    private static readonly int ANIMATOR_PICKINGUP = Animator.StringToHash("picking up");

    private void Update()
    {
        float horizontalInput = Input.GetAxis(playerId.ToString());

        animator.SetFloat(ANIMATOR_SPEED, Mathf.Abs(horizontalInput));

        animator.SetBool(ANIMATOR_GROUNDED, groundCheck.IsGrounded);
    }

    public void ShootAnimation()
    {
        StartCoroutine(ShootAnimationCo());
    }

    private IEnumerator ShootAnimationCo()
    {
        animator.SetBool(ANIMATOR_SHOOTING, true);

        yield return new WaitForSeconds(.2f);

        animator.SetBool(ANIMATOR_SHOOTING, false);
    }

    public void StartPickUpAnimation()
    {
        animator.SetBool(ANIMATOR_PICKINGUP, true);
    }

    public void StopPickUpAnimation()
    {
        animator.SetBool(ANIMATOR_PICKINGUP, false);
    }
}
