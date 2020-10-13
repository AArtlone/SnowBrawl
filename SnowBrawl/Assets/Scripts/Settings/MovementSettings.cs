using UnityEngine;

[CreateAssetMenu(fileName = "MovementSettings", menuName = "MovementSettings")]
public class MovementSettings : ScriptableObject
{
    public float speed;
    public float poweredUpSpeed;
    public float springAbility;
    public float fallMultiplier;
    public float minSpeed;
    public float maxSpeed;
}
