using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPickingUpCharacter : TutorialCharacter
{
    [SerializeField] protected List<GameObject> snowballIcons = new List<GameObject>(2);

    private static readonly int ANIMATOR_IDLE = Animator.StringToHash("Idle");
    private static readonly int ANIMATOR_PICKUP = Animator.StringToHash("PickUp");

    protected override void Awake()
    {
        base.Awake();

        UpdateSnowballs(2);
    }

    private void OnDisable()
    {
        StopAllCoroutines();

        UpdateSnowballs(2);
    }

    private void OnEnable()
    {
        StartCoroutine(PickUp());
    }

    private IEnumerator PickUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            
            animator.Play(ANIMATOR_PICKUP);

            yield return new WaitForSeconds(.5f);

            snowballIcons[0].SetActive(true);

            UpdateSnowballs(1);

            yield return new WaitForSeconds(.5f);

            snowballIcons[1].SetActive(true);

            UpdateSnowballs(0);
            
            animator.Play(ANIMATOR_IDLE);

            yield return new WaitForSeconds(1f);

            snowballIcons.ForEach(s => s.SetActive(false));

            UpdateSnowballs(2);
        }
    }

    protected virtual void UpdateSnowballs(int value)
    {

    }
}
