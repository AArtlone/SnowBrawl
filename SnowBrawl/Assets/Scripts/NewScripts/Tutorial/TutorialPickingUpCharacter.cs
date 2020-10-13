using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialPickingUpCharacter : TutorialCharacter
{
    [SerializeField] protected List<GameObject> snowballIcons = new List<GameObject>(2);

    [SerializeField] private List<EffectBase> droppedSnowballsIcons = new List<EffectBase>(2);

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
        animator.Play(ANIMATOR_IDLE);
        
        StartCoroutine(PickUp());
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

            StartCoroutine(DropSnowballs());
        }
    }

    private void ResetPage()
    {
        walk = false;

        travelTime = 0;

        transform.position = spawnPos;

        snowballIcons.ForEach(s => s.SetActive(false));

        snowballsText.text = 0.ToString();

        droppedSnowballsIcons.ForEach(i => 
        {
            i.gameObject.SetActive(false);
        });
    }

    private IEnumerator PickUp()
    {
        yield return new WaitForSeconds(1f);

        animator.Play(ANIMATOR_PICKUP);

        yield return new WaitForSeconds(.5f);

        snowballIcons[0].SetActive(true);

        yield return new WaitForSeconds(.5f);

        snowballIcons[1].SetActive(true);

        animator.Play(ANIMATOR_WALK);

        walk = true;
    }

    private IEnumerator DropSnowballs()
    {
        // TODO: play sound

        animator.Play(ANIMATOR_IDLE);

        snowballIcons[1].SetActive(false);
        droppedSnowballsIcons[0].gameObject.SetActive(true);

        snowballsText.text = 1.ToString();

        yield return new WaitForSeconds(.5f);

        snowballIcons[0].SetActive(false);

        droppedSnowballsIcons[1].gameObject.SetActive(true);

        snowballsText.text = 2.ToString();

        yield return new WaitForSeconds(1f);

        RepeatSequence();
    }

    private void RepeatSequence()
    {
        ResetPage();

        animator.Play(ANIMATOR_IDLE);

        StartCoroutine(PickUp());
    }
}
