using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerArea : MonoBehaviour
{
    public float damage = 1;
    void OnTriggerEnter(Collider collider)
    {
        switch (collider.tag)
        {
            case "Destructible":
                collider.GetComponent<Destructible>().DestroyObject();
                break;
            case "Player":
                if (!Char.Instance.dash)
                {
                    Char.Instance.UpdateHealth(-damage);
                }
                break;
            case "Enemy":
                collider.GetComponent<Enemy>().UpdateHealth(-damage);
                break;
            default:
                break;
        }
    }
}