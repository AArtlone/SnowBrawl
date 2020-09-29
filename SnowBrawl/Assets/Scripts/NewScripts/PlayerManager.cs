using UnityEngine;

public class PlayerManager: MonoBehaviour
{
    private Player playerID;

    private void OnTriggerEnter2D(Collider2D col)
    {
        CheckForBaseEnter(col);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        CheckForBaseExit(col);
    }

    private void CheckForBaseEnter(Collider2D col)
    {
        PickableBase pickableBase = col.GetComponent<PickableBase>();
        if (pickableBase != null)
        {
            Debug.Log(pickableBase.CanPickUp);
        }
    }

    private void CheckForBaseExit(Collider2D col)
    {
        PickableBase playerBase = col.GetComponent<PickableBase>();
        
    }
}
