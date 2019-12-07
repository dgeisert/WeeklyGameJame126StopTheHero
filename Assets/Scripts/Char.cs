﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char : MonoBehaviour
{
    public static Char Instance;
    public InGameUI gameUI;
    public float speed;
    public float health = 5;
    public Scarf healthScarf;
    int layerMask = 1 << 8;

    public float mana;
    public float manaMax;
    public Scarf manaScarf;
    public float recharge;
    public float waitRecharge;
    float lastManaSpend;

    public Transform projectileSpawnPoint;

    public Projectile primaryProjectile;

    public Projectile altProjectile;

    public bool dash = false;
    public float dashCost;
    public float dashCooldown;
    public float dashDuration;
    public float dashSpeed;
    float lastDash;
    bool canRecharge;

    WeaponPickup activePickup;

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameUI.UpdateMana(mana, manaMax);
        UpdateHealth(0);
        UpdateMana(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Game.Instance.active)
        {
            return;
        }
        //Player movement
        if (Controls.Forward && !Controls.Back)
        {
            Move(Vector3.forward);
        }
        else if (Controls.Back && !Controls.Forward)
        {
            Move(-Vector3.forward);
        }
        if (Controls.Right && !Controls.Left)
        {
            Move(Vector3.right);
        }
        else if (Controls.Left && !Controls.Right)
        {
            Move(-Vector3.right);
        }

        // Player Rotation
        Look();

        //Other player controls
        if (Controls.PickUpDropPrimary)
        {
            PickUpDrop(true);
        }
        if (Controls.PickUpDropAlt)
        {
            PickUpDrop(false);
        }
        if (Controls.Dash && lastDash + dashDuration + dashCooldown < Time.time)
        {
            Dash();
        }
        if (Controls.Shoot)
        {
            Shoot(primaryProjectile);
        }
        if (Controls.AltShoot)
        {
            Shoot(altProjectile);
        }

        //Player recharge and deactivations
        if (lastDash + dashDuration < Time.time)
        {
            dash = false;
        }
        if (lastManaSpend + waitRecharge < Time.time && mana < manaMax)
        {
            UpdateMana(recharge * Time.deltaTime);
        }
    }

    public void Move(Vector3 dir)
    {
        transform.position += dir * Time.unscaledDeltaTime * speed * (dash ? dashSpeed : 1);
    }

    public void PickUpDrop(bool primary)
    {
        if (activePickup)
        {
            Instantiate((primary ? primaryProjectile : altProjectile).pickup.gameObject, transform.position, Quaternion.identity);
            if (primary)
            {
                primaryProjectile = activePickup.weapon;
            }
            else
            {
                altProjectile = activePickup.weapon;
            }
            Destroy(activePickup.gameObject);
            activePickup = null;
        }
    }
    public void Dash()
    {
        if (UpdateMana(-dashCost))
        {
            dash = true;
            lastDash = Time.time;
        }
    }
    public void Shoot(Projectile projectile)
    {
        if (UpdateMana(-projectile.cost))
        {
            Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation).player = true;
        }
    }

    public void Look()
    {
        RaycastHit hit = GetMousePoint();
        transform.LookAt(hit.point);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }

    public RaycastHit GetMousePoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 50f, layerMask))
        {
            Transform objectHit = hit.transform;
        }
        return hit;
    }

    bool UpdateMana(float amount)
    {
        if (amount < 0)
        {
            lastManaSpend = Time.time;
            if (mana + amount < 0)
            {
                return false;
            }
        }
        mana += amount;
        if (mana > manaMax)
        {
            mana = manaMax;
        }
        manaScarf.SetLength(mana / 20);
        return true;
    }

    public void UpdateHealth(float amount)
    {
        health += amount;
        if (amount < 0)
        {
            if (health < 0)
            {
                Game.Instance.GameOver();
            }
        }
        healthScarf.SetLength(health);
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Pickup")
        {
            activePickup = collider.GetComponent<WeaponPickup>();
        }
    }
    public void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Pickup")
        {
            if (activePickup == collider.GetComponent<WeaponPickup>())
            {
                activePickup = null;
            }
        }
    }
}