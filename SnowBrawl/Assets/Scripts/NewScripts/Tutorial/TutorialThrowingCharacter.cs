using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TutorialThrowingCharacter : MonoBehaviour
{
    [SerializeField] private TutorialSnowball snowballPrefab;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private float throwFrequency;
    [SerializeField] private int speed;

    private static readonly int ANIMATOR_SHOOTING = Animator.StringToHash("shooting");

    private Animator animator;

    private float currentTime;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= throwFrequency)
        {
            Throw();

            currentTime = 0;
        }
    }

    private void Throw()
    {
        StartCoroutine(ShootAnimationCo());
    }

    private IEnumerator ShootAnimationCo()
    {
        animator.SetBool(ANIMATOR_SHOOTING, true);

        yield return new WaitForSeconds(.2f);

        TutorialSnowball snowball = Instantiate(snowballPrefab, shootingPoint.position, Quaternion.identity);

        snowball.Shoot(-Vector2.right, speed);

        animator.SetBool(ANIMATOR_SHOOTING, false);
    }
}
