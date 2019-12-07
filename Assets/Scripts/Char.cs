using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char : MonoBehaviour
{
    public InGameUI gameUI;
    public float speed;
    int layerMask = 1 << 8;

    public float mana;
    public float manaMax;
    public float recharge;
    public float waitRecharge;
    float lastManaSpend;
    public float altCost;

    bool dash = false;
    public float dashCost;
    public float dashCooldown;
    public float dashDuration;
    public float dashSpeed;
    float lastDash;
    bool canRecharge;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
        if (Controls.PickUpDrop)
        {
            PickUpDrop();
        }
        if (Controls.Interact)
        {
            Interact();
        }
        if (Controls.Dash && lastDash + dashDuration + dashCooldown < Time.time)
        {
            Dash();
        }

        //Player recharge and deactivations
        if (lastDash + dashDuration < Time.time)
        {
            dash = false;
        }
        if (lastManaSpend + waitRecharge < Time.time && mana < manaMax)
        {
            if (mana + recharge * Time.deltaTime > manaMax)
            {
                UpdateMana(manaMax - mana);
            }
            else
            {
                UpdateMana(recharge * Time.deltaTime);
            }
        }
    }

    public void Move(Vector3 dir)
    {
        transform.position += dir * Time.unscaledDeltaTime * speed * (dash ? dashSpeed : 1);
    }

    public void PickUpDrop()
    {

    }
    public void Interact()
    {

    }
    public void Dash()
    {
        if (UpdateMana(-dashCost))
        {
            dash = true;
            lastDash = Time.time;
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
            if (mana - amount < 0)
            {
                return false;
            }
        }
        mana += amount;
        gameUI.UpdateMana(mana, manaMax);
        return true;
    }
}