using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MeshDivider : MonoBehaviour
{
    Vector3 scale = Vector3.one;
    public Vector3 newScale = Vector3.one;
    Mesh mesh;
    public float newComplexity = 5;
    float complexity = 5;
    public Color newColor;
    Color color;
    public int newSeed;
    int seed;
    BoxCollider col;
    public List<PlaceObject> placeObjects;
    public List<PlaceObject> interestingObjects;
    public float objectDensity = 0.05f;
    public float interestingObjectDensity = 0.05f;
    public bool randomize = true;

    void Start()
    {
        col = GetComponent<BoxCollider>();
        if (randomize)
        {
            newSeed = Mathf.FloorToInt(Random.value * 10000);
        }
        seed = newSeed;
        scale = newScale;
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        UpdateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        if (scale != newScale || complexity != newComplexity || color != newColor || seed != newSeed)
        {
            seed = newSeed;
            Random.InitState(seed);
            scale = newScale;
            color = newColor;
            complexity = newComplexity;
            UpdateMesh();
        }
    }

    void UpdateMesh()
    {
        if (scale.x == 0 || scale.y == 0 || complexity == 0)
        {
            return;
        }
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        List<Color> newColors = new List<Color>();
        List<Vector3> newVertices = new List<Vector3>();
        List<Vector3> newVertices2 = new List<Vector3>();
        List<int> newTriangles = new List<int>();
        int iMax = Mathf.FloorToInt((scale.x) * complexity) + 1;
        int jMax = Mathf.FloorToInt((scale.y) * complexity) + 1;

        for (int i = 0; i < iMax; i++)
        {
            for (int j = 0; j < jMax; j++)
            {
                bool rand = j != 0 && i != 0 && j != jMax - 1 && i != iMax - 1;
                newVertices.Add(new Vector3(
                    (-(scale.x / 2) + (i + (rand ? (Random.value - 0.5f) : 0)) / complexity),
                    (-(scale.y / 2) + (j + (rand ? (Random.value - 0.5f) : 0)) / complexity),
                    (rand ? (Random.value - 0.5f) : 0) * scale.z
                ));
                if (rand && Random.value < objectDensity && placeObjects.Count > 0)
                {
                    PlaceObject po = Instantiate(placeObjects[Mathf.FloorToInt(Random.value * placeObjects.Count)]);
                    po.transform.SetParent(transform);
                    po.Setup(new Vector3(newVertices[jMax * i + j].x, newVertices[jMax * i + j].y, 0), scale.y);
                }
                if (i > 0)
                {
                    if (j != jMax - 1)
                    {
                        newVertices2.Add(newVertices[jMax * i + j]);
                        newTriangles.Add(newVertices2.Count - 1);
                        newVertices2.Add(newVertices[jMax * (i - 1) + j]);
                        newTriangles.Add(newVertices2.Count - 1);
                        newVertices2.Add(newVertices[jMax * (i - 1) + j + 1]);
                        newTriangles.Add(newVertices2.Count - 1);
                        newColors.Add(color);
                        newColors.Add(color);
                        newColors.Add(color);
                    }
                    if (j > 0)
                    {
                        newVertices2.Add(newVertices[jMax * i + j]);
                        newTriangles.Add(newVertices2.Count - 1);
                        newVertices2.Add(newVertices[jMax * i + j - 1]);
                        newTriangles.Add(newVertices2.Count - 1);
                        newVertices2.Add(newVertices[jMax * (i - 1) + j]);
                        newTriangles.Add(newVertices2.Count - 1);
                        newColors.Add(color);
                        newColors.Add(color);
                        newColors.Add(color);
                    }
                }
            }
        }
        if (interestingObjects.Count > 0)
        {
            for (int i = 1; i < iMax - 1; i++)
            {
                if (Random.value < interestingObjectDensity)
                {
                    PlaceObject po = Instantiate(interestingObjects[Mathf.FloorToInt(Random.value * interestingObjects.Count)]);
                    po.transform.SetParent(transform);
                    po.Setup(new Vector3(-(scale.x / 2) + (i / complexity), scale.y / 2, 0), scale.y);
                    i += 3;
                }
            }
        }

        //put black backing on wall
        newVertices2.Add(new Vector3(
            (scale.x / 2) - 1, (scale.y / 2), 0.5f
        ));
        newTriangles.Add(newVertices2.Count - 1);
        newVertices2.Add(new Vector3(
            (scale.x / 2), (scale.y / 2), 0
        ));
        newTriangles.Add(newVertices2.Count - 1);
        newVertices2.Add(new Vector3(-(scale.x / 2) + 1, (scale.y / 2), 0.5f));
        newTriangles.Add(newVertices2.Count - 1);
        newColors.Add(Color.black);
        newColors.Add(Color.black);
        newColors.Add(Color.black);

        newVertices2.Add(new Vector3(
            (scale.x / 2), (scale.y / 2), 0
        ));
        newTriangles.Add(newVertices2.Count - 1);
        newVertices2.Add(new Vector3(-(scale.x / 2), (scale.y / 2), 0));
        newTriangles.Add(newVertices2.Count - 1);
        newVertices2.Add(new Vector3(-(scale.x / 2) + 1, (scale.y / 2), 0.5f));
        newTriangles.Add(newVertices2.Count - 1);
        newColors.Add(Color.black);
        newColors.Add(Color.black);
        newColors.Add(Color.black);

        newVertices2.Add(new Vector3(
            (scale.x / 2) - 1, -(scale.y / 2), 0.5f
        ));
        newTriangles.Add(newVertices2.Count - 1);
        newVertices2.Add(new Vector3(
            (scale.x / 2) - 1, (scale.y / 2), 0.5f
        ));
        newTriangles.Add(newVertices2.Count - 1);
        newVertices2.Add(new Vector3(-(scale.x / 2) + 1, -(scale.y / 2), 0.5f));
        newTriangles.Add(newVertices2.Count - 1);
        newColors.Add(Color.black);
        newColors.Add(Color.black);
        newColors.Add(Color.black);

        newVertices2.Add(new Vector3(
            (scale.x / 2) - 1, (scale.y / 2), 0.5f
        ));
        newTriangles.Add(newVertices2.Count - 1);
        newVertices2.Add(new Vector3(-(scale.x / 2) + 1, (scale.y / 2), 0.5f));
        newTriangles.Add(newVertices2.Count - 1);
        newVertices2.Add(new Vector3(-(scale.x / 2) + 1, -(scale.y / 2), 0.5f));
        newTriangles.Add(newVertices2.Count - 1);
        newColors.Add(Color.black);
        newColors.Add(Color.black);
        newColors.Add(Color.black);

        newVertices2.Add(new Vector3(
            (scale.x / 2), (scale.y / 2), 0f
        ));
        newTriangles.Add(newVertices2.Count - 1);
        newVertices2.Add(new Vector3(
            (scale.x / 2) - 1, (scale.y / 2), 0.5f
        ));
        newTriangles.Add(newVertices2.Count - 1);
        newVertices2.Add(new Vector3(
            (scale.x / 2) - 1, -(scale.y / 2), 0.5f
        ));
        newTriangles.Add(newVertices2.Count - 1);
        newColors.Add(Color.black);
        newColors.Add(Color.black);
        newColors.Add(Color.black);

        newVertices2.Add(new Vector3(
            (scale.x / 2), -(scale.y / 2), 0
        ));
        newTriangles.Add(newVertices2.Count - 1);
        newVertices2.Add(new Vector3(
            (scale.x / 2), (scale.y / 2), 0
        ));
        newTriangles.Add(newVertices2.Count - 1);
        newVertices2.Add(new Vector3(
            (scale.x / 2) - 1, -(scale.y / 2), 0.5f
        ));
        newTriangles.Add(newVertices2.Count - 1);
        newColors.Add(Color.black);
        newColors.Add(Color.black);
        newColors.Add(Color.black);

        newVertices2.Add(new Vector3(-(scale.x / 2) + 1, (scale.y / 2), 0.5f));
        newTriangles.Add(newVertices2.Count - 1);
        newVertices2.Add(new Vector3(-(scale.x / 2), (scale.y / 2), 0f));
        newTriangles.Add(newVertices2.Count - 1);
        newVertices2.Add(new Vector3(-(scale.x / 2) + 1, -(scale.y / 2), 0.5f));
        newTriangles.Add(newVertices2.Count - 1);
        newColors.Add(Color.black);
        newColors.Add(Color.black);
        newColors.Add(Color.black);

        newVertices2.Add(new Vector3(-(scale.x / 2), (scale.y / 2), 0));
        newTriangles.Add(newVertices2.Count - 1);
        newVertices2.Add(new Vector3(-(scale.x / 2), -(scale.y / 2), 0));
        newTriangles.Add(newVertices2.Count - 1);
        newVertices2.Add(new Vector3(-(scale.x / 2) + 1, -(scale.y / 2), 0.5f));
        newTriangles.Add(newVertices2.Count - 1);
        newColors.Add(Color.black);
        newColors.Add(Color.black);
        newColors.Add(Color.black);

        mesh.Clear();
        mesh.vertices = newVertices2.ToArray();
        mesh.triangles = newTriangles.ToArray();
        mesh.colors = newColors.ToArray();
        mesh.RecalculateNormals();

        if (col != null)
        {
            col.size = scale + Vector3.forward;
            col.center = Vector3.forward * 0.35f;
        }
    }
}