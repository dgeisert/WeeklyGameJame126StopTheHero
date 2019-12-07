using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scarf : MonoBehaviour
{
    public List<Vector3> mods = new List<Vector3>();
    public List<Vector3> points = new List<Vector3>();
    public float length;
    LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            mods.Add(lineRenderer.GetPosition(i) * 2);
        }
        lineRenderer.positionCount = 0;
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
        lineRenderer.positionCount = Mathf.FloorToInt(length) + 1;
        for (int i = 0; i < Mathf.FloorToInt(length) + 1; i++)
        {
            lineRenderer.SetPosition(i, transform.position +
                (mods[i] - Vector3.up * 1.5f) +
                (points[points.Count - 1 - i] - transform.position) +
                (Vector3.one * Mathf.Sin(-Time.time * 2 + i)) / 15);
        }
    }

    public void SetLength(float setLength)
    {
        length = setLength;
    }
}