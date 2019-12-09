using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawns : MonoBehaviour
{
    public List<PlaceObject> roomEdges;
    public int edgeCount;
    public List<PlaceObject> roomCenter;
    public int centerCount;
    // Start is called before the first frame update
    public void Setup(RoomCreator rc)
    {
        if (roomEdges.Count > 0 && edgeCount > 0)
        {
            for (int i = 0; i < edgeCount; i++)
            {
                bool vert = Random.value > 0.5f;
                Vector3 pos = new Vector3(
                    (vert ? (Random.value > 0.5f ? -1 : 1) * (rc.setSize.x / 2 - 0.5f) : (Random.value - 0.5f) * rc.setSize.x * 0.75f),
                    Random.value / 100,
                    (!vert ? (Random.value > 0.5f ? -1 : 1) * (rc.setSize.y / 2 - 0.5f) : (Random.value - 0.5f) * rc.setSize.y* 0.75f)
                );
                Vector3 euler = new Vector3(0, vert ? (pos.x > 0 ? 90 : -90) : (pos.z > 0 ? 0 : 180), 0);
                CreateObject(pos, roomEdges[Mathf.FloorToInt(roomEdges.Count * Random.value)]).transform.eulerAngles = euler;
            }
        }
        if (roomCenter.Count > 0 && centerCount > 0)
        {
            for (int i = 0; i < centerCount; i++)
            {
                Vector3 pos = new Vector3(
                    ((Random.value - 0.5f) * rc.setSize.x * 0.6f),
                    0,
                    ((Random.value - 0.5f) * rc.setSize.y * 0.6f)
                );
                CreateObject(pos, roomCenter[Mathf.FloorToInt(roomCenter.Count * Random.value)]);
            }
        }
    }

    PlaceObject CreateObject(Vector3 pos, PlaceObject prefab)
    {
        PlaceObject po = Instantiate(prefab);
        po.transform.SetParent(transform);
        po.Setup(pos, 0);
        return po;
    }
}