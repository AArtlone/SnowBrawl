using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector2 endPos;
    [SerializeField] private float timeToTarget;

    private float travelTime;
    private bool reverse;

    private void OnCollisionEnter2D(Collision2D col)
    {
        Player player = col.gameObject.GetComponent<Player>();

        if (player == null)
            return;

        player.transform.parent = transform;
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        Player player = col.gameObject.GetComponent<Player>();

        if (player == null)
            return;

        player.transform.parent = null;
    }

    private void Update()
    {
        if (GameManager.Instance.GameIsPaused)
            return;

        travelTime += Time.deltaTime;

        float t = travelTime / timeToTarget;

        float distanceToTarget;
        if (reverse)
        {
            transform.position = Vector2.Lerp(endPos, startPos, t);

            distanceToTarget = Vector2.Distance(transform.position, startPos);
        }
        else
        {
            transform.position = Vector2.Lerp(startPos, endPos, t);

            distanceToTarget = Vector2.Distance(transform.position, endPos);
        }

        if (distanceToTarget < .001f)
            ChangeDirection();
    }

    private void ChangeDirection()
    {
        reverse = !reverse;

        travelTime = 0;
    }
}
