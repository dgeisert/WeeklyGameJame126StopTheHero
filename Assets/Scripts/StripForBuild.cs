using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class StripForBuild : MonoBehaviour
{
    public static StripForBuild Instance;
    public static bool Strip
    {
        get
        {
#if UNITY_EDITOR
            if (Instance)
            {
                return Instance.strip;
            }
            return false;
#else
            return false;
#endif
        }
    }
#if UNITY_EDITOR
    public bool strip;
    void Update()
    {
        Instance = this;
    }
#endif
}