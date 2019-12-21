using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightArea : MonoBehaviour
{
    public Enemy enemy;
    void OnTriggerEnter(Collider collider)
    {
        if (!enemy.triggered)
        {
            if (collider.tag == "Player")
            {
                RaycastHit hit;
                Physics.Raycast(enemy.transform.position,
                    (collider.transform.position - enemy.transform.position),
                    out hit);
                if (hit.collider.tag == "Player")
                {
                    enemy.Shout();
                    enemy.Trigger();
                }
            }
        }
    }
}