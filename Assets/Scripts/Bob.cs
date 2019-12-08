using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour
{
    public Vector3 rot, pos;
    public float rate;
    Vector3 startRot;
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startRot = transform.eulerAngles;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPos + new Vector3(0, Mathf.Sin(Time.time * rate) * pos.y, 0);
        transform.eulerAngles = startRot + new Vector3(Mathf.Sin(Time.time * rate * 2) * rot.x, Mathf.Sin(Time.time * rate) * rot.y, Mathf.Sin(Time.time * rate * 2) * rot.z);
    }
}