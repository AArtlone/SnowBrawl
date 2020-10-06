using System.Collections.Generic;
using UnityEngine;

public class ControlsSelection : MonoBehaviour
{
    [SerializeField] private KeysSettings p1KeysSettings;
    [SerializeField] private KeysSettings p2KeysSettings;

    [SerializeField] private List<ControlListener> allListeners;

    [SerializeField] private GameObject confirmationPopUp;
    [SerializeField] private GameObject selectAllBindingsPopUp;

    private ControlListener controlListener;

    private PlayerID selectedPlayer = PlayerID.P1;

    private bool listeningToControlSelection;

    private void Awake()
    {
        LoadView();
    }

    private void OnGUI()
    {
        if (!listeningToControlSelection)
            return;

        Event e = Event.current;

        if (e.isKey && Input.GetKeyDown(e.keyCode))
        {
            CheckIfIsTaken(e.keyCode);

            listeningToControlSelection = false;

            KeysSettings keysSettings;

            if (selectedPlayer == PlayerID.P1)
                keysSettings = p1KeysSettings;
            else
                keysSettings = p2KeysSettings;

            switch (controlListener.GameAction)
            {
                case GameAction.Jump:
                    keysSettings.jumpKey = e.keyCode;
                    break;
                case GameAction.Throw:
                    keysSettings.throwKey = e.keyCode;
                    break;
                case GameAction.PickUp:
                    keysSettings.pickUpKey = e.keyCode;
                    break;
                case GameAction.Drop:
                    keysSettings.dropKey = e.keyCode;
                    break;
            }

            controlListener.UpdateBinding(e.keyCode);

            controlListener = null;
        }
    }

    private void LoadView()
    {
        foreach (ControlListener controlListener in allListeners)
        {
            KeysSettings keysSettings;

            if (controlListener.PlayerID == PlayerID.P1)
                keysSettings = p1KeysSettings;
            else
                keysSettings = p2KeysSettings;

            switch (controlListener.GameAction)
            {
                case GameAction.Drop:
                    controlListener.UpdateBinding(keysSettings.dropKey);
                    break;
                case GameAction.Throw:
                    controlListener.UpdateBinding(keysSettings.throwKey);
                    break;
                case GameAction.Jump:
                    controlListener.UpdateBinding(keysSettings.jumpKey);
                    break;
                case GameAction.PickUp:
                    controlListener.UpdateBinding(keysSettings.pickUpKey);
                    break;
            }
        }
    }

    public void ListenToControl(GameObject obj)
    {
        controlListener = obj.GetComponent<ControlListener>();

        if (controlListener == null)
            return;

        listeningToControlSelection = true;
    }

    public void StartGame()
    {
        SBSceneManager.Instance.LoadFirstRound();
    }

    public void ShowConfirmationPopUp()
    {
        if (!CheckIfAllBindingsWereAssigned())
        {
            confirmationPopUp.SetActive(false);

            selectAllBindingsPopUp.SetActive(true);
        }
        else
        {
            confirmationPopUp.SetActive(true);
        }
    }

    public void ClosePopUp(GameObject obj)
    {
        obj.SetActive(false);
    }

    private bool CheckIfAllBindingsWereAssigned()
    {
        foreach (ControlListener controlListener in allListeners)
        {
            if (controlListener.KeyBinding == KeyCode.None)
                return false;
        }

        return true;
    }

    /// <summary>
    /// Check if the key binding is taken already
    /// </summary>
    /// <param name="keyCode"></param>
    private void CheckIfIsTaken(KeyCode keyCode)
    {
        foreach (ControlListener controlListener in allListeners)
        {
            if (controlListener.KeyBinding != keyCode)
                continue;

            KeysSettings keysSettings;

            if (selectedPlayer == PlayerID.P1)
                keysSettings = p1KeysSettings;
            else
                keysSettings = p2KeysSettings;

            switch (controlListener.GameAction)
            {
                case GameAction.Jump:
                    keysSettings.jumpKey = KeyCode.None;
                    break;
                case GameAction.Throw:
                    keysSettings.throwKey = KeyCode.None;
                    break;
                case GameAction.PickUp:
                    keysSettings.pickUpKey = KeyCode.None;
                    break;
                case GameAction.Drop:
                    keysSettings.dropKey = KeyCode.None;
                    break;
            }
            controlListener.ResetBinding();
        }
    }
}