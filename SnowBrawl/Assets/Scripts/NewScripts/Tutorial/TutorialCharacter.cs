using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TutorialCharacter : MonoBehaviour
{
    protected Animator animator;
    protected Vector2 spawnPos;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();

        spawnPos = transform.position;
    }
}
