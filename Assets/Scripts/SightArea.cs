using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightArea : MonoBehaviour
{
    public Enemy enemy;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            enemy.triggered = true;
        }
    }
}