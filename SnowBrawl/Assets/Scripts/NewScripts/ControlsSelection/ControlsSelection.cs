using System.Collections.Generic;
using UnityEngine;

public class ControlsSelection : MonoBehaviour
{
    [SerializeField] private KeysSettings p1KeysSettings;
    [SerializeField] private KeysSettings p2KeysSettings;

    private bool listeningToControlSelection;

    private Dictionary<GameAction, KeyCode> dictionary;

    private ControlListener controlListener;

    private void Awake()
    {
        dictionary = new Dictionary<GameAction, KeyCode>(4);

        dictionary.Add(GameAction.Jump, KeyCode.W);
        dictionary.Add(GameAction.Drop, KeyCode.R);
        dictionary.Add(GameAction.Throw, KeyCode.T);
        dictionary.Add(GameAction.PickUp, KeyCode.Y);
    }

    public void ListenToControl(GameObject obj)
    {
        controlListener = obj.GetComponent<ControlListener>();

        if (controlListener == null)
            return;

        listeningToControlSelection = true;
    }

    private void OnGUI()
    {
        if (!listeningToControlSelection)
            return;

        Event e = Event.current;

        if (e.isKey && Input.GetKeyDown(e.keyCode))
        {
            listeningToControlSelection = false;

            dictionary[controlListener.GameAction] = e.keyCode;

            controlListener.UpdateText(e.keyCode);
        }
    }

    private void Update()
    {
        if (!listeningToControlSelection)
            return;

        //foreach (KeyCode kc in System.Enum.GetValues(typeof(KeyCode)))
        //{
        //    if (Input.GetKeyDown(kc))
        //        print(kc);
        //}
    }

    public void ConfirmSettings()
    {
        p1KeysSettings.Init(
            dictionary[GameAction.Jump],
            dictionary[GameAction.Throw],
            dictionary[GameAction.PickUp],
            dictionary[GameAction.Drop]
        );
    }
}