using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public static Controls Instance;
    public static bool Forward
    {
        get
        {
            return Input.GetKey(KeyCode.W) ||
                Input.GetKey(KeyCode.UpArrow);
        }
    }
    public static bool Left
    {
        get
        {
            return Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.LeftArrow);
        }
    }
    public static bool Right
    {
        get
        {
            return Input.GetKey(KeyCode.D) ||
                Input.GetKey(KeyCode.RightArrow);
        }
    }
    public static bool Back
    {
        get
        {
            return Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.DownArrow);
        }
    }
    public static bool Dash
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }
    public static bool Shoot
    {
        get
        {
            return Input.GetMouseButtonDown(0);
        }
    }
    public static bool AltShoot
    {
        get
        {
            return Input.GetMouseButtonDown(1);
        }
    }
    public static bool PickUpDropPrimary
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Q);
        }
    }
    public static bool PickUpDropAlt
    {
        get
        {
            return Input.GetKeyDown(KeyCode.E);
        }
    }
    public static bool Pause
    {
        get
        {
            return Input.GetKeyDown(KeyCode.P);
        }
    }
}