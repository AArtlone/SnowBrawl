using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialStealingCharacter : TutorialCharacter
{
    [SerializeField] protected List<GameObject> snowballIcons = new List<GameObject>(2);

    [SerializeField] private TextMeshProUGUI snowballsText;

    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector2 targetPos;
    [SerializeField] private float timeToTarget;

    private static readonly int ANIMATOR_IDLE = Animator.StringToHash("Idle");
    private static readonly int ANIMATOR_PICKUP = Animator.StringToHash("PickUp");
    private static readonly int ANIMATOR_WALK = Animator.StringToHash("Walk");

    private bool walk;

    private float travelTime;

    private void OnDisable()
    {
        StopAllCoroutines();

        ResetPage();
    }

    private void OnEnable()
    {
        animator.Play(ANIMATOR_WALK);

        walk = true;
    }

    private void Update()
    {
        if (!walk)
            return;

        travelTime += Time.deltaTime;

        float t = travelTime / timeToTarget;

        transform.localPosition = Vector2.Lerp(startPos, targetPos, t);

        float distanceToTarget = Vector2.Distance(transform.localPosition, targetPos);

        if (distanceToTarget < .001f)
        {
            walk = false;

            travelTime = 0;

            animator.Play(ANIMATOR_IDLE);

            StartCoroutine(PickUp());
        }
    }

    private IEnumerator PickUp()
    {
        yield return new WaitForSeconds(.5f);

        animator.Play(ANIMATOR_PICKUP);

        yield return new WaitForSeconds(.5f);

        snowballIcons[0].SetActive(true);
        snowballsText.text = 1.ToString();
        SoundManager.PlaySound(Sound.PickUpSnowball);

        yield return new WaitForSeconds(.5f);

        snowballIcons[1].SetActive(true);
        snowballsText.text = 0.ToString();
        SoundManager.PlaySound(Sound.PickUpSnowball);

        animator.Play(ANIMATOR_IDLE);

        yield return new WaitForSeconds(1f);

        RepeatSequence();
    }

    private void ResetPage()
    {
        walk = false;

        travelTime = 0;

        transform.position = spawnPos;

        snowballIcons.ForEach(s => s.SetActive(false));

        snowballsText.text = 2.ToString();
    }

    private void RepeatSequence()
    {
        ResetPage();

        animator.Play(ANIMATOR_WALK);

        walk = true;
    }
}
