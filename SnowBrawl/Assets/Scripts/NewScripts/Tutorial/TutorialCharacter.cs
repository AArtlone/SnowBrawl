using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TutorialCharacter : MonoBehaviour
{
    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
