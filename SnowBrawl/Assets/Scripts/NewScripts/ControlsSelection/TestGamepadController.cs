using System.Collections.Generic;
using UnityEngine;

public class TestGamepadController : MonoBehaviour
{
    public static TestGamepadController Instance;

    private List<MyKey> myKeys;

    private const string P1_A_BUTTON_AXIS = "P1 AButton";
    private const string P1_B_BUTTON_AXIS = "P1 BButton";
    private const string P1_X_BUTTON_AXIS = "P1 XButton";
    private const string P1_Y_BUTTON_AXIS = "P1 YButton";

    private const string P2_A_BUTTON_AXIS = "P2 AButton";
    private const string P2_B_BUTTON_AXIS = "P2 BButton";
    private const string P2_X_BUTTON_AXIS = "P2 XButton";
    private const string P2_Y_BUTTON_AXIS = "P2 YButton";

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        InitMyKeys();
    }

    private void InitMyKeys()
    {
        myKeys = new List<MyKey>
        {
            new MyKey(P1_A_BUTTON_AXIS, KeyCode.Joystick1Button0),
            new MyKey(P1_B_BUTTON_AXIS, KeyCode.Joystick1Button1),
            new MyKey(P1_X_BUTTON_AXIS, KeyCode.Joystick1Button2),
            new MyKey(P1_Y_BUTTON_AXIS, KeyCode.Joystick1Button3),
                      
            // Player P2
            new MyKey(P2_A_BUTTON_AXIS, KeyCode.Joystick2Button0),
            new MyKey(P2_B_BUTTON_AXIS, KeyCode.Joystick2Button1),
            new MyKey(P2_X_BUTTON_AXIS, KeyCode.Joystick2Button2),
            new MyKey(P2_Y_BUTTON_AXIS, KeyCode.Joystick2Button3)
        };
    }

    public bool GetKeyDown(KeyCode keyCode)
    {
        foreach (var myKey in myKeys)
            if (myKey.keyCode == keyCode)
                return myKey.GetKeyDown();

        return false;
    }

    public bool GetKey(KeyCode keyCode)
    {
        foreach (var myKey in myKeys)
            if (myKey.keyCode == keyCode)
                return myKey.GetKey();

        return false;
    }

    public bool GetKeyUp(KeyCode keyCode)
    {
        foreach (var myKey in myKeys)
            if (myKey.keyCode == keyCode)
                return myKey.GetKeyUp();

        return false;
    }

    private void Update()
    {
        foreach (var myKey in myKeys)
            myKey.UpdateKeyStatus();
    }
}

//TODO: Name properly and put in the separate class
public class MyKey
{
    private string buttonAxis;

    public KeyCode keyCode;

    private bool isPressed; // PF = previous frame
    private bool previousFrameIsPressed;

    public MyKey(string buttonAxis, KeyCode keyCode)
    {
        this.buttonAxis = buttonAxis;
        this.keyCode = keyCode;
    }

    public void UpdateKeyStatus()
    {
        previousFrameIsPressed = isPressed;
        isPressed = Input.GetAxisRaw(buttonAxis) == 1;
    }

    public bool GetKeyDown()
    {
        if (isPressed && !previousFrameIsPressed)
            return true;

        return false;
    }

    public bool GetKey()
    {
        return isPressed;
    }

    public bool GetKeyUp()
    {
        if (!isPressed && previousFrameIsPressed)
            return true;

        return false;
    }
}
