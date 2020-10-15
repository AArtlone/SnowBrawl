using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsSelection : MonoBehaviour
{
    [SerializeField] private KeysSettings p1KeysSettings;
    [SerializeField] private KeysSettings p2KeysSettings;

    [SerializeField] private List<ControlListener> allListeners;

    [SerializeField] private GameObject confirmationPopUp;
    [SerializeField] private GameObject selectAllBindingsPopUp;

    [SerializeField] private TabButton p1Button;
    [SerializeField] private TabButton p2Button;

    [Space(5), SerializeField] private GameObject dimmedBackground;

    private ControlListener controlListener;
    private Canvas controlListenerCanvas;

    private PlayerID selectedPlayer = PlayerID.P1;

    private bool listeningToControlSelection;

    private void Awake()
    {
        LoadView();

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(p1KeysSettings);
        UnityEditor.EditorUtility.SetDirty(p2KeysSettings);
#endif
        p1Button.onTabSelected += OnPlayer1TabSelected;
        p2Button.onTabSelected += OnPlayer2TabSelected;
    }

    private void OnDestroy()
    {
        p1Button.onTabSelected -= OnPlayer1TabSelected;
        p2Button.onTabSelected -= OnPlayer2TabSelected;
    }

    private void OnPlayer1TabSelected()
    {
        selectedPlayer = PlayerID.P1;
    }

    private void OnPlayer2TabSelected()
    {
        selectedPlayer = PlayerID.P2;
    }

    private void OnGUI()
    {
        if (!listeningToControlSelection)
            return;

        Event e = Event.current;

        if (!e.isKey || !Input.GetKeyDown(e.keyCode))
            return;

        SaveNewKeyBinding(e.keyCode);
    }

    private void Update()
    {
        if (!listeningToControlSelection)
            return;

        var pressedCustomkey = SBInputManager.Instance.IsAnyCustomKeyIsDown();

        if (pressedCustomkey == KeyCode.None)
            return;

        SaveNewKeyBinding(pressedCustomkey);
    }

    private void SaveNewKeyBinding(KeyCode keyCode)
    {
        CheckIfIsTaken(keyCode);

        listeningToControlSelection = false;

        dimmedBackground.SetActive(false);

        Destroy(controlListenerCanvas);

        KeysSettings keysSettings;

        if (selectedPlayer == PlayerID.P1)
            keysSettings = p1KeysSettings;
        else
            keysSettings = p2KeysSettings;

        switch (controlListener.GameAction)
        {
            case GameAction.Jump:
                keysSettings.jumpKey = keyCode;
                break;
            case GameAction.Throw:
                keysSettings.throwKey = keyCode;
                break;
            case GameAction.PickUp:
                keysSettings.pickUpKey = keyCode;
                break;
            case GameAction.Drop:
                keysSettings.dropKey = keyCode;
                break;
        }

        controlListener.UpdateBinding(keyCode);

        controlListener = null;
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

        dimmedBackground.SetActive(true);

        controlListenerCanvas = controlListener.gameObject.AddComponent<Canvas>();

        controlListenerCanvas.overrideSorting = true;

        controlListenerCanvas.sortingOrder = 10;
    }

    public void LoadTutorial()
    {
        SBSceneManager.Instance.LoadTutorial();
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