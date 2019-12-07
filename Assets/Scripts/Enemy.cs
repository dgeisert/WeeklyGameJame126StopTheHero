using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public Projectile attack;
    public float range;
    public float attackCooldown;
    float lastAttack;
    public Transform attackSpawnLoc;
    public float speed;
    public GameObject deathParticles;
    public bool triggered;
    public List<Transform> patrolPath;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        if (attack.lifetime > 0)
        {
            range = Mathf.Min(range, attack.lifetime * attack.speed - 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered)
        {
            target = Char.Instance.transform;
            if (Vector3.Distance(transform.position, target.position) <= range)
            {
                Face();
                if (lastAttack + attackCooldown < Time.time)
                {
                    Attack();
                }
            }
            else
            {
                Face();
                Move();
            }
        }
    }

    void Face()
    {
        transform.LookAt(target.position);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }

    void Move()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    void Attack()
    {
        Instantiate(attack, attackSpawnLoc.position, attackSpawnLoc.rotation);
        lastAttack = Time.time;
    }

    public void UpdateHealth(float amount)
    {
        health += amount;
        if (amount < 0)
        {
            if (health < 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        if (deathParticles != null)
        {
            Instantiate(deathParticles, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}