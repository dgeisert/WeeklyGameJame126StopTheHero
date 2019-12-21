using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    List<RoomCreator> rooms = new List<RoomCreator>();
    int frame;
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            RoomCreator rc = transform.GetChild(i).GetComponent<RoomCreator>();
            if (rc != null)
            {
                rooms.Add(rc);
                rc.gameObject.SetActive(Vector3.Distance(rc.transform.position, Char.Instance.transform.position) < 30);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        frame++;
        if (frame > 60)
        {
            frame = 0;
            foreach (RoomCreator rc in rooms)
            {
                rc.gameObject.SetActive(Vector3.Distance(rc.transform.position, Char.Instance.transform.position) < 30);
            }
        }
    }
}