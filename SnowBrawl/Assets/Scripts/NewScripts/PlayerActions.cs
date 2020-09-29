using UnityEngine;

public class PlayerActions: MonoBehaviour
{
    [SerializeField] private Player player;

    private KeyCode pickUpKey = KeyCode.P;
    private KeyCode throwKey = KeyCode.T;
    private KeyCode drop = KeyCode.B;

    private void Update()
    {
        CheckForActions();   
    }

    public void CheckForActions()
    {
        if (Input.GetKeyDown(pickUpKey))
            player.PickUp();
        else if (Input.GetKeyDown(throwKey))
            player.Throw();
        else if (Input.GetKeyDown(drop))
            player.Drop();
    }
}
