using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public int amount;
    public void Activate(Vector3 pos)
    {
        Game.Score += amount;
    }
}