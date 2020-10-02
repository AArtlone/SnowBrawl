using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlListener : MonoBehaviour
{
    [SerializeField] private GameAction gameAction;
    [SerializeField] private TextMeshProUGUI keyCodeText;

    public GameAction GameAction { get { return gameAction; } }

    public void UpdateText(KeyCode keyCode)
    {
        keyCodeText.text = keyCode.ToString();
    }
}