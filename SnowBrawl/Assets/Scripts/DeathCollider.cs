using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();

        if (player == null)
            return;

        GameManager.Instance.KillPlayer(player);
    }
}
