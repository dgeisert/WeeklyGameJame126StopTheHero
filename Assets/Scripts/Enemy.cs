using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public Scarf healthScarf;
    float hitTime;
    public Projectile attack;
    public float range;
    public float attackCooldown;
    float lastAttack;
    public Transform attackSpawnLoc;
    public float speed;
    public GameObject deathParticles;
    public bool triggered;
    public List<Transform> patrolPath;
    int patrolPoint;
    float wanderTime;
    float wanderDir;
    Transform target;
    public AudioClip alertAudio;
    public AudioClip ouchAudio;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        transform.eulerAngles = Vector3.up * Random.value * 360;
        if (patrolPath.Count > 0)
        {
            target = patrolPath[patrolPoint];
        }
        Game.Instance.enemies.Add(this);
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
        else if (patrolPath.Count > 0)
        {
            if (Vector3.Distance(target.position, transform.position) < 0.5f)
            {
                patrolPoint++;
                if (patrolPoint >= patrolPath.Count)
                {
                    patrolPoint = 0;
                }
                target = patrolPath[patrolPoint];
            }
            Face();
            Move();
        }
        else
        {
            wanderTime += Time.deltaTime;
            if (wanderTime > 4 && Random.value < 0.05f)
            {
                wanderTime = 0;
                wanderDir = Random.value - 0.5f;
            }
            else if (wanderTime < 0.5f)
            {
                Move();
            }
            else if (wanderTime > 3)
            {
                transform.localEulerAngles += new Vector3(0, wanderDir * Time.deltaTime * 200, 0);
            }
        }
        Knockback();
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
            hitTime = Time.time;
            audioSource.clip = ouchAudio;
            audioSource.Play();
            if (!triggered)
            {
                health += amount;
            }
            if (health <= 0)
            {
                Die();
            }
            else if (!triggered)
            {
                Shout();
            }
            triggered = true;
        }
        if (healthScarf != null)
        {
            healthScarf.SetLength(health);
        }
    }

    public void Knockback()
    {
        if (hitTime + 0.1f > Time.time && speed > 0)
        {
            transform.position -= transform.forward * Time.deltaTime * 4;
        }
    }

    void Die()
    {
        if (deathParticles != null)
        {
            Instantiate(deathParticles, transform.position, transform.rotation);
        }
        Game.Instance.enemies.Remove(this);
        Destroy(gameObject);
    }

    public void Shout()
    {
        foreach (Enemy e in Game.Instance.enemies)
        {
            if (e != null && Vector3.Distance(e.transform.position, transform.position) < 6)
            {
                e.triggered = true;
            }
        }
        audioSource.clip = alertAudio;
        audioSource.Play();
    }
}