using UnityEngine;
using System.Collections;

public class InputHelper : MonoBehaviour {

    public static InputHelper instance;

    #if UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX
    public enum Button
    {
        A = 0,
        B = 1,
        X = 2,
        Y = 3,
        START = 7
    }
    #elif UNITY_STANDALONE_OSX
    public enum Button
    {
        A = 16,
        B = 17,
        X = 18,
        Y = 19,
        START = 9
    }
    #endif

    void Awake()
    {
        if( instance == null )
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.Log("Duplicate instance detected, destroying gameObject");
            Destroy(gameObject);
        }
    }

    public string GetInputButtonString(int controller, Button button)
    {
        int buttonNum = (int)button;
        return "joystick " + controller.ToString() + " button " + buttonNum.ToString();
    }

    public float GetAngle(int controller)
    {
        float horiz = Input.GetAxis("P" + controller.ToString() + "_Horiz");
        // Multiply by -1 to have the positive angle going upwards
        float vert = Input.GetAxis("P" + controller.ToString() + "_Vert") * -1;

        float angle = Mathf.Atan2(vert, horiz) * Mathf.Rad2Deg;

        if( angle < 0 )
            angle += 360;

        return angle;
    }

    public float GetHorizForController(int controller)
    {
        return Input.GetAxis("P" + controller.ToString() + "_Horiz");
    }

    public float GetVertForController(int controller)
    {
        // Multiply by -1 to have the positive angle going upwards
        return Input.GetAxis("P" + controller.ToString() + "_Vert") * -1;
    }
}
