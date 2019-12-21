using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public int amount;
    public List<WeaponPickup> weapons = new List<WeaponPickup>();
    public void Activate(Vector3 pos)
    {
        Game.Score += amount;
        if (weapons.Count > 0)
        {
            Instantiate(weapons[Mathf.FloorToInt(Random.value * weapons.Count)], pos, Quaternion.identity);
        }
    }
}