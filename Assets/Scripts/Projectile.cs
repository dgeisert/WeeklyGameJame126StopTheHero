using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool player = false;
    public bool spinX, spinY, spinZ;
    public float spinSpeed = 5;
    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (spinX)
        {
            transform.eulerAngles += Vector3.right * spinSpeed * Time.deltaTime * 360;
        }
        if (spinY)
        {
            transform.eulerAngles += Vector3.up * spinSpeed * Time.deltaTime * 360;
        }
        if (spinZ)
        {
            transform.eulerAngles += Vector3.forward * spinSpeed * Time.deltaTime * 360;
        }
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    public void OnTriggerEnter(Collider col)
    {
        
    }
}