using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject particles;
    public Reward reward;
    public void DestroyObject()
    {
        if (particles != null)
        {
            Destroy(Instantiate(particles, transform.position, transform.rotation), 1);
        }
        if (reward != null)
        {
            reward.Activate(transform.position);
        }
        Destroy(gameObject);
    }
}