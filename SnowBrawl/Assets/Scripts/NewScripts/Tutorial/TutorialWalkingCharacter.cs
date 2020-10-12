using UnityEngine;

public class TutorialWalkingCharacter : MonoBehaviour
{
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector2 targetPos;
    [SerializeField] private float timeToTarget;

    [SerializeField] private bool facingRight;

    private float travelTime;

    private float scaleValue;

    private void Awake()
    {
        scaleValue = transform.localScale.y;
    }

    private void Update()
    {
        travelTime += Time.deltaTime;

        float t = travelTime / timeToTarget;

        float distanceToTarget;

        if (!facingRight)
        {
            transform.localPosition = Vector2.Lerp(targetPos, startPos, t);

            distanceToTarget = Vector2.Distance(transform.localPosition, startPos);
        }
        else
        {
            transform.localPosition = Vector2.Lerp(startPos, targetPos, t);

            distanceToTarget = Vector2.Distance(transform.localPosition, targetPos);
        }

        if (distanceToTarget < .001f)
            ChangeDirection();
    }

    private void ChangeDirection()
    {
        travelTime = 0;

        facingRight = !facingRight;

        if (!facingRight)
            transform.localScale = new Vector3(-scaleValue, scaleValue, scaleValue);
        else
            transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
    }
}
