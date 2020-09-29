using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBase : PickableBase
{
    public override bool CanPickUp
    {
        get
        {
            return base.CanPickUp;
        }

        protected set
        {
            base.CanPickUp = true;
        }
    }

    private void Start()
    {
        // You can always pick up from the MainBase
        CanPickUp = true;
    }
}
