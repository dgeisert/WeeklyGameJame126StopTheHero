using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance;
    public Transform target;
    public float smoothing = 1;
    Vector3 pos;
    public bool rot = false;
    public float shakeTime;
    public float shakeAmount;
    float startShake;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }
    public void Setup()
    {
        pos = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + pos, Time.deltaTime * smoothing);
        if (startShake + shakeTime > Time.time)
        {
            transform.position += new Vector3(Random.value - 0.5f, Random.value - 0.5f, Random.value - 0.5f) * Time.deltaTime * shakeAmount;
        }
    }

    public void Shake()
    {
        startShake = Time.time;
    }
}