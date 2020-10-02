using UnityEngine;

[CreateAssetMenu(fileName = "KeysSettings", menuName = "KeysSettings")]
public class KeysSettings : ScriptableObject
{
    public KeyCode jumpKey;
    public KeyCode throwKey;
    public KeyCode pickUpKey;
    public KeyCode dropKey;

    public void Init(KeyCode jumpKey, KeyCode throwKey, KeyCode pickUpKey, KeyCode dropKey)
    {
        this.jumpKey = jumpKey;
        this.throwKey = throwKey;
        this.pickUpKey = pickUpKey;
        this.dropKey = dropKey;
    }
}
