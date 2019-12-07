using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string textName;
    public Transform visualObject;
    public bool player = false;
    public bool spinX, spinY, spinZ;
    public float spinSpeed = 5;
    public float speed = 10;
    public float lifetime = -1;
    public float damage = 1;
    public float cost;
    public WeaponPickup pickup;
    float spawnTime;
    public GameObject deathParticles;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (visualObject != null)
        {
            if (spinX)
            {
                visualObject.eulerAngles += Vector3.right * spinSpeed * Time.deltaTime * 360;
            }
            if (spinY)
            {
                visualObject.eulerAngles += Vector3.up * spinSpeed * Time.deltaTime * 360;
            }
            if (spinZ)
            {
                visualObject.eulerAngles += Vector3.forward * spinSpeed * Time.deltaTime * 360;
            }
        }
        transform.position += transform.forward * speed * Time.deltaTime;
        if (lifetime > 0 && spawnTime + lifetime < Time.time)
        {
            DestroyProjectile();
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        switch (col.tag)
        {
            case "Wall":
                DestroyProjectile();
                break;
            case "Destructible":
                col.GetComponent<Destructible>().DestroyObject();
                DestroyProjectile();
                break;
            case "Player":
                if (!player && !Char.Instance.dash)
                {
                    Char.Instance.UpdateHealth(-damage);
                    DestroyProjectile();
                }
                break;
            case "Enemy":
                if (player)
                {
                    col.GetComponent<Enemy>().UpdateHealth(-damage);
                    DestroyProjectile();
                }
                break;
            default:
                break;
        }
    }

    void DestroyProjectile(Collider col = null)
    {
        if (deathParticles != null)
        {
            Destroy(Instantiate(deathParticles, transform.position, col != null ? col.transform.rotation : transform.rotation), 2);
        }
        Destroy(gameObject);
    }
}