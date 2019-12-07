using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scarf : MonoBehaviour
{
    public List<Transform> mods = new List<Transform>();
    public List<Vector3> points = new List<Vector3>();
    LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        mods.Add(transform);
        Transform t = transform;
        while (t.childCount > 0)
        {
            mods.Add(t.GetChild(0));
            t = t.GetChild(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        points.Add(transform.position);
        if (points.Count < mods.Count)
        {
            return;
        }
        points.Remove(points[0]);
        lineRenderer.positionCount = points.Count;
        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, transform.position + 
            (mods[i].transform.position - transform.position) + 
            (points[points.Count - 1 - i] - transform.position) + 
            (Vector3.one * Mathf.Sin(Time.time * 2 + i)) / 15);
        }
    }
}