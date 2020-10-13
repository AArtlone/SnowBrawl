using TMPro;
using UnityEngine;

public class ControlListener : MyButton
{
    [SerializeField] private GameAction gameAction;
    [SerializeField] private TextMeshProUGUI keyCodeText;
    [SerializeField] private PlayerID playerID;

    public GameAction GameAction { get { return gameAction; } }
    public PlayerID PlayerID { get { return playerID; } }

    public KeyCode KeyBinding { get; private set; }

    public void UpdateBinding(KeyCode keyCode)
    {
        KeyBinding = keyCode;

        UpdateText();
    }

    public void ResetBinding()
    {
        KeyBinding = KeyCode.None;

        UpdateText();
    }

    private void UpdateText()
    {
        string bindingText = SBInputManager.Instance.ConvertBindingToText(KeyBinding);

        keyCodeText.text = bindingText;
    }
}