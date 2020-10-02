using TMPro;
using UnityEngine;

public class ControlListener : MonoBehaviour
{
    [SerializeField] private GameAction gameAction;
    [SerializeField] private TextMeshProUGUI keyCodeText;
    [SerializeField] private PlayerID playerID;

    public GameAction GameAction { get { return gameAction; } }
    public PlayerID PlayerID { get { return playerID; } }

    public void UpdateText(string text)
    {
        keyCodeText.text = text;
    }
}