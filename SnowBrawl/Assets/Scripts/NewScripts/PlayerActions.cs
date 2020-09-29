using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions
{
    private KeyCode pickUpKey;
    private KeyCode throwKey;
    private KeyCode drop;

    public void CheckForActions()
    {
        if (Input.GetKey(pickUpKey))
            PickUp();
        else if (Input.GetKey(throwKey))
            Throw();
        else if (Input.GetKey(drop))
            Drop();
    }

    private void PickUp()
    {

    }

    private void Throw()
    {

    }

    private void Drop()
    {

    }
}
