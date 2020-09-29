using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickableBase : MonoBehaviour
{
    public virtual bool CanPickUp { get; protected set; }
}
