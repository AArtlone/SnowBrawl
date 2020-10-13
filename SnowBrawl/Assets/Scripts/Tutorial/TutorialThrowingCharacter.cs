using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TutorialThrowingCharacter : TutorialCharacter
{
    [SerializeField] private GameObject snowballIcon;
    [SerializeField] private TutorialSnowball snowballPrefab;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private float throwFrequency;
    [SerializeField] private int speed;

    private static readonly int ANIMATOR_IDLE = Animator.StringToHash("Idle");
    private static readonly int ANIMATOR_THROW = Animator.StringToHash("Throw");

    private float currentTime;

    private bool firstShotFired;

    private List<GameObject> snowballs = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        snowballIcon.SetActive(true);
    }

    private void OnEnable()
    {
        animator.Play(ANIMATOR_IDLE);
    }

    private void OnDisable()
    {
        ResetCharacter();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (!firstShotFired && currentTime > 1f)
        {
            Throw();

            firstShotFired = true;
        }

        if (currentTime < throwFrequency)
            return;

        Throw();
    }

    private void ResetCharacter()
    {
        snowballs.ForEach(s => Destroy(s));

        StopAllCoroutines();

        snowballIcon.SetActive(true);

        currentTime = 0;

        firstShotFired = false;
    }

    private void Throw()
    {
        currentTime = 0;

        snowballIcon.SetActive(false);

        StartCoroutine(ThrowAnimationCo());
    }

    private IEnumerator ThrowAnimationCo()
    {
        animator.Play(ANIMATOR_THROW);

        float delay = animator.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(delay + .1f);

        TutorialSnowball snowball = Instantiate(snowballPrefab, shootingPoint.position, Quaternion.identity);

        snowball.Shoot(-Vector2.right, speed);

        snowballs.Add(snowball.gameObject);

        SoundManager.PlaySound(Sound.ThrowSnowball);

        animator.Play(ANIMATOR_IDLE);

        yield return new WaitForSeconds(1f);
        
        snowballIcon.SetActive(true);
    }
}
