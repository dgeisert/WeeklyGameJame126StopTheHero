using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public TMPro.TextMeshPro infoDisplay;
    public Projectile weapon;
    // Start is called before the first frame update
    void Start()
    {
        infoDisplay.text = weapon.textName +
            "\nDamage: " + weapon.damage +
            "\nRange: " + (weapon.lifetime > 0 ? (weapon.lifetime * weapon.speed) : 100) +
            "\nMana: " + weapon.cost;
        infoDisplay.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            infoDisplay.gameObject.SetActive(true);
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            infoDisplay.gameObject.SetActive(false);
        }
    }
}