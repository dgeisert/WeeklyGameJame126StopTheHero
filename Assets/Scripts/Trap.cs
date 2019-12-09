using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public DamagePlayerArea damagePlayerArea;
    public float duration;
    public GameObject vfx;
    public Projectile projectile;
    public List<Transform> spawnPoints;

    public void Trigger()
    {
        if (projectile != null)
        {
            foreach (Transform t in spawnPoints)
            {
                Instantiate(projectile, t.position, t.rotation).speed /= 2;
            }
        }
        else if (damagePlayerArea != null)
        {
            StartCoroutine(ResetTrap());
        }
        if (vfx != null)
        {
            vfx.SetActive(true);
        }
    }

    public IEnumerator ResetTrap()
    {
        yield return new WaitForSeconds(duration);
    }
}