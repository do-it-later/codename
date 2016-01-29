﻿using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public static InputManager instance;

    #if UNITY_STANDALONE_WIN
    public enum Button
    {
        A = 0,
        B = 1,
        X = 2,
        Y = 3
    }
    #elif UNITY_STANDALONE_OSX
    public enum Button
    {
        A = 16,
        B = 17,
        X = 18,
        Y = 19
    }
    #endif

    void Awake()
    {
        instance = this;
    }

    public string GetInputButtonString(int controller, Button button)
    {
        int buttonNum = (int)button;
        return "joystick " + controller.ToString() + " button " +   buttonNum.ToString();
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
}
