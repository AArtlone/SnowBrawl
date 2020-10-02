using System.Collections.Generic;
using UnityEngine;

public class ControlsSelection : MonoBehaviour
{
    [SerializeField] private KeysSettings p1KeysSettings;
    [SerializeField] private KeysSettings p2KeysSettings;

    [SerializeField] private GameObject p1View;
    [SerializeField] private GameObject p2View;

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
            CheckIfBusy(e.keyCode);

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

    public void StartGame()
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

    private void CheckIfBusy(KeyCode keyCode)
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

    public void ListenToControl(GameObject obj)
    {
        controlListener = obj.GetComponent<ControlListener>();

        if (controlListener == null)
            return;

        listeningToControlSelection = true;
    }

    public void SelectPlayer(int playerID)
    {
        if (playerID == 1 && selectedPlayer != PlayerID.P1)
        {
            selectedPlayer = PlayerID.P1;
            UpdateView();
        }
        else if (playerID == 2 && selectedPlayer != PlayerID.P2)
        {
            selectedPlayer = PlayerID.P2;
            UpdateView();
        }
    }

    private void UpdateView()
    {
        p1View.SetActive(selectedPlayer == PlayerID.P1);
        p2View.SetActive(selectedPlayer == PlayerID.P2);
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
}