using System.Collections.Generic;
using UnityEngine;

public class SBInputManager : MonoBehaviour
{
    public static SBInputManager Instance;

    private List<CustomKey> myKeys;

    private const string P1_A_BUTTON_AXIS = "P1 AButton";
    private const string P1_B_BUTTON_AXIS = "P1 BButton";
    private const string P1_X_BUTTON_AXIS = "P1 XButton";
    private const string P1_Y_BUTTON_AXIS = "P1 YButton";
    private const string P1_LB_BUTTON_AXIS = "P1 LBButton";
    private const string P1_RB_BUTTON_AXIS = "P1 RBButton";

    private const string P2_A_BUTTON_AXIS = "P2 AButton";
    private const string P2_B_BUTTON_AXIS = "P2 BButton";
    private const string P2_X_BUTTON_AXIS = "P2 XButton";
    private const string P2_Y_BUTTON_AXIS = "P2 YButton";
    private const string P2_LB_BUTTON_AXIS = "P2 LBButton";
    private const string P2_RB_BUTTON_AXIS = "P2 RBButton";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;

            DontDestroyOnLoad(this);
        }

        InitMyKeys();
    }

    private void Update()
    {
        foreach (var myKey in myKeys)
            myKey.UpdateKeyStatus();
    }

    private void InitMyKeys()
    {
        myKeys = new List<CustomKey>
        {
            new CustomKey(P1_A_BUTTON_AXIS, KeyCode.Joystick1Button0),
            new CustomKey(P1_B_BUTTON_AXIS, KeyCode.Joystick1Button1),
            new CustomKey(P1_X_BUTTON_AXIS, KeyCode.Joystick1Button2),
            new CustomKey(P1_Y_BUTTON_AXIS, KeyCode.Joystick1Button3),
            new CustomKey(P1_LB_BUTTON_AXIS, KeyCode.Joystick1Button4),
            new CustomKey(P1_RB_BUTTON_AXIS, KeyCode.Joystick1Button5),
                      
            // Player P2
            new CustomKey(P2_A_BUTTON_AXIS, KeyCode.Joystick2Button0),
            new CustomKey(P2_B_BUTTON_AXIS, KeyCode.Joystick2Button1),
            new CustomKey(P2_X_BUTTON_AXIS, KeyCode.Joystick2Button2),
            new CustomKey(P2_Y_BUTTON_AXIS, KeyCode.Joystick2Button3),
            new CustomKey(P2_LB_BUTTON_AXIS, KeyCode.Joystick2Button4),
            new CustomKey(P2_RB_BUTTON_AXIS, KeyCode.Joystick2Button5)
        };
    }

    public float GetPlayerInput(PlayerID id)
    {
        string stringID = id.ToString();

        float horizontalInput = Input.GetAxisRaw(stringID + " Keyboard");

        if (horizontalInput == 0)
            horizontalInput = Input.GetAxisRaw(stringID);

        return horizontalInput;
    }

    public bool AnyKeyDown()
    {
        if (Input.anyKeyDown)
            return true;

        if (IsAnyCustomKeyIsDown() != KeyCode.None)
            return true;

        return false;
    }

    public KeyCode IsAnyCustomKeyIsDown()
    {
        foreach (var customKey in myKeys)
        {
            if (customKey.GetKeyDown())
                return customKey.keyCode;
        }

        return KeyCode.None;
    }

    public bool GetKeyDown(KeyCode keyCode)
    {
        if (!IsMyKey(keyCode))
            return Input.GetKeyDown(keyCode);

        foreach (var myKey in myKeys)
        {
            if (myKey.keyCode == keyCode)
                return myKey.GetKeyDown();
        }

        return false;
    }

    public bool GetKey(KeyCode keyCode)
    {
        if (!IsMyKey(keyCode))
            return Input.GetKey(keyCode);

        foreach (var myKey in myKeys)
        {
            if (myKey.keyCode == keyCode)
                return myKey.GetKey();
        }

        return false;
    }

    public bool GetKeyUp(KeyCode keyCode)
    {
        if (!IsMyKey(keyCode))
            return Input.GetKeyUp(keyCode);

        foreach (var myKey in myKeys)
        {
            if (myKey.keyCode == keyCode)
                return myKey.GetKeyUp();
        }

        return false;
    }

    private bool IsMyKey(KeyCode keyCode)
    {
        foreach (var v in myKeys)
        {
            if (v.keyCode == keyCode)
                return true;
        }

        return false;
    }
}
