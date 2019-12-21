using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Char : MonoBehaviour
{
    public static Char Instance;
    public CameraFollow cam;
    public float speed;
    Vector3 mov;
    public float health = 5;
    public float invicibleAfterHit = 0.2f;
    float lastHit;
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
    AudioSource audioSource;
    public AudioClip dashAudio;
    public AudioClip ouchAudio;

    WeaponPickup activePickup;

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateHealth(0);
        UpdateMana(0);
        SceneManager.sceneLoaded += OnSceneLoaded;
        Setup();
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        Setup();
    }

    void Setup()
    {
        Time.timeScale = 1;
        Game.Instance.active = true;
        transform.position = Vector3.zero;
        cam = Camera.main.GetComponent<CameraFollow>();
        if (cam != null)
        {
            cam.target = transform;
            cam.Setup();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Game.Instance.active)
        {
            return;
        }
        //Player movement
        mov = Vector3.zero;
        if (Controls.Forward && !Controls.Back)
        {
            mov += Vector3.forward;
        }
        else if (Controls.Back && !Controls.Forward)
        {
            mov -= Vector3.forward;
        }
        if (Controls.Right && !Controls.Left)
        {
            mov += Vector3.right;
        }
        else if (Controls.Left && !Controls.Right)
        {
            mov -= Vector3.right;
        }
        Move(mov.normalized);

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
            audioSource.clip = dashAudio;
            audioSource.Play();
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
        if (amount < 0 && lastHit + invicibleAfterHit > Time.time)
        {
            return;
        }
        health += amount;
        if (amount < 0)
        {
            lastHit = Time.time;
            audioSource.clip = ouchAudio;
            audioSource.Play();
            if (cam != null)
            {
                cam.Shake();
            }
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