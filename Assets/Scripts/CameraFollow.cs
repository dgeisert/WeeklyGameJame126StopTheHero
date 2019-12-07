using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 1;
    Vector3 pos;
    public bool rot = false;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + pos, Time.deltaTime * smoothing);
    }
}
