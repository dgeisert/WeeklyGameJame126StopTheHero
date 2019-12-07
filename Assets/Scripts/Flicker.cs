using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    Light light;
    float intensity;
    public float varaition;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        intensity = light.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        light.intensity = intensity + (Mathf.Sin(Time.time) + Mathf.Sin(Time.time * 17) / 3 + Mathf.Sin(Time.time * 13) / 5) * varaition;
    }
}
