using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    public float exactY = -1;
    public float offWall = 0;
    public void Setup(Vector3 pos, float wallHeight)
    {
        if (exactY >= 0)
        {
            transform.localPosition = new Vector3(pos.x, exactY - wallHeight / 2 + Random.value / 100, pos.z - offWall + Random.value / 100);
        }
        else
        {
            transform.localPosition = pos - Vector3.forward * (offWall + Random.value / 100);
        }
        transform.localRotation = Quaternion.identity;
    }
}