using UnityEngine;

[CreateAssetMenu(fileName = "KeysSettings", menuName = "KeysSettings")]
public class KeysSettings : ScriptableObject
{
    public KeyCode jumpKey;
    public KeyCode throwKey;
    public KeyCode pickUpKey;
    public KeyCode dropKey;
}
