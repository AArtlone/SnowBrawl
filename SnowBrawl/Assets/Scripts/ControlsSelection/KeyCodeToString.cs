using UnityEngine;

public static class KeyCodeToString
{
    public static string Convert(KeyCode keyCode)
    {
        if (keyCode == KeyCode.Joystick1Button0 || keyCode == KeyCode.Joystick2Button0)
            return "Joystick A";
        else if (keyCode == KeyCode.Joystick1Button1 || keyCode == KeyCode.Joystick2Button1)
            return "Joystick B";
        else if (keyCode == KeyCode.Joystick1Button2 || keyCode == KeyCode.Joystick2Button2)
            return "Joystick X";
        else if (keyCode == KeyCode.Joystick1Button3 || keyCode == KeyCode.Joystick2Button3)
            return "Joystick Y";
        else if (keyCode == KeyCode.Joystick1Button4 || keyCode == KeyCode.Joystick2Button4)
            return "Joystick LB";
        else if (keyCode == KeyCode.Joystick1Button5 || keyCode == KeyCode.Joystick2Button5)
            return "Joystick RB";

        return string.Empty;
    }
}
