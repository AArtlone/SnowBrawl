using UnityEngine;

public class CustomKey
{
    private string buttonAxis;

    public KeyCode keyCode;

    private bool isPressed; // PF = previous frame
    private bool previousFrameIsPressed;

    public CustomKey(string buttonAxis, KeyCode keyCode)
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
