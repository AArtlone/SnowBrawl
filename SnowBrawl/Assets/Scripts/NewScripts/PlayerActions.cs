using UnityEngine;

public class PlayerActions: MonoBehaviour
{
    [SerializeField] private Player player;

    private KeyCode pickUpKey;
    private KeyCode throwKey;
    private KeyCode dropKey;

    private void Awake()
    {
        pickUpKey = player.KeysSettings.pickUpKey;
        throwKey = player.KeysSettings.throwKey;
        dropKey = player.KeysSettings.dropKey;
    }

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
        else if (Input.GetKeyDown(dropKey))
            player.Drop();
    }
}
