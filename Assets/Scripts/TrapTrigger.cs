using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public Trap trap;
    public void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.name);
        if (collider.tag == "Player")
        {
            trap.Trigger();
        }
    }
}